using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed;

    public bool isWalking;
    public bool isRunning;

    [SerializeField] private GameInput gameInput;
    private void Update() {

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
        Debug.Log(Time.deltaTime);

    }

    public bool IsWalking() {

        return isWalking;
    }
    public bool IsRunning() {
        return isRunning;
    }
}
