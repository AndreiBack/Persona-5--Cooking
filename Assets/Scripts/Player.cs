using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    private static Player instance; 
    public static Player Instance { get; private set; }

    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayermask;

    public float moveSpeed;
    public bool isWalking;
    public bool isRunning;
    public bool isHolding;

    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    public EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    private void Awake() {
        if (instance != null)
        {
            Debug.LogError("Theres more than 1 player instance");
        }
        Instance = this;
    }
    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

  private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
    if (selectedCounter != null) {
        selectedCounter.Interact(this);
    } 
}



    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {

        return isWalking;
    }
    public bool IsRunning() {
        return isRunning;
    }
    public bool IsHolding() {
        return isHolding;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // Levanta o ponto de origem do Raycast

        if (Physics.Raycast(rayOrigin, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayermask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
        }
        else
        {
            // Se o Raycast não encontrar mais um contador, deseleciona
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement() {


        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f;
        float playerHeight = 2f;
        //fisicas de capsula
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //mover diagonalmente se não tiver parede na frente
        if (!canMove)
        { // pode mover no X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {// pode mover no X

                moveDir = moveDirX;
            }

            else
            {// não pode mover no x, mas pode mover no Z

                Vector3 moveDirZ = new Vector3(moveDir.z, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {// pode mover no Z

                    moveDir = moveDirZ;
                }
                else
                {
                    //nao pode mover em nenhuma direção
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }


        isWalking = moveDir != Vector3.zero;
        isRunning = moveDir != Vector3.zero && Input.GetKey(KeyCode.LeftShift);

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

   private void SetSelectedCounter(BaseCounter selectedCounter) {
   
    this.selectedCounter = selectedCounter;

    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
        selectedCounter = selectedCounter
    });
}




    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        isHolding = true;
        ;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
