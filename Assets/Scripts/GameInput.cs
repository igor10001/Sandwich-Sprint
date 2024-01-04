using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputAction;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInput playerInput;
    private void Awake()
    {

        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
        //there we need to set up logic to determinate what player handle input and when to spawn 
        
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;

    }

 
}
