using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    private PlayerInputActions playerInputAction;
    private Vector2 inputVector;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
   private Player player;

     private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();//get player input component
        var players = FindObjectsOfType<Player>();//earches the scene for an active object of type Player and returns the first one it finds. 
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        
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

    public void OnMove(CallbackContext context)
    {
        if (player != null)
        {

            player.SetInputVector(context.ReadValue<Vector2>().normalized);
        }
    }
   

  /*  public Vector2 GetMovementVectorNormalized()
    {
       
        return inputVector.normalized;

    }*/

 
}
