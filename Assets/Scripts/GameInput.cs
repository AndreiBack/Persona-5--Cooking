using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions playerInputActions;
   [SerializeField] private Player player;




    void Awake() {
        player = FindFirstObjectByType<Player>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Interact_Alternate.performed += Interact_Alternate_performed;
    }

    private void Interact_Alternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        if (player.isWalking)
        {
            player.moveSpeed = 3f;
        }
        if (player.isRunning)
        {
            player.moveSpeed = 8f;
        }

        inputVector = inputVector.normalized;

        return inputVector;
    }


}
