using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();
    }

  public Vector2 GetMovementVectorNormalized()
  {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;

  }

 
}
