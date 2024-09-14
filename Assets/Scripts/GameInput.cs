using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
   [SerializeField] private Player player;




    void Awake() {
        player = FindFirstObjectByType<Player>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
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
