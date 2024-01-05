using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

    

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float moveSpeed = 7f;
    //[SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private int playerIndex = 0;

    private Vector2 inputVector;

   //public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    
    BaseCounter selectedCounter ;
    private Vector3 lastInteractedDirection;

    private bool isWalking;
    private KitchenObject kitchenObject;

    private void Awake()
    {/*
        if(Instance != null) 
        {
            Debug.LogError("there s more than one Player instance");
        }
        Instance = this;*/
    }

    private void Start()
    {
        GameInput.instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManagerr.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) 
        {
            selectedCounter.InteractAlternate(this);
        }

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManagerr.Instance.IsGamePlaying() ) return;
       if(selectedCounter!= null)
        {
            selectedCounter.Interact(this);
        }
       

    }


    private void Update()
    {
        HandleInteractions();
        HandleMovement();
    }

    public bool IsWalking()
    { return isWalking; }

    private void HandleMovement()
    {
       // Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Debug.Log("input Vector " + inputVector);
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        Debug.Log("moveDir " + moveDir);
        if (moveDir != Vector3.zero)
        {
            lastInteractedDirection = moveDir;
        }
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
                //can move only on the z

            }
            else
            {
                //cannon move only on the x
                //atteampt only z 
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //   Debug.Log("Cannon move in any direction");
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 15f;
        if(moveDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);


    }

    private void HandleInteractions()
    {


      //  Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

       

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractedDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter selectedCounter))
            {
                if (selectedCounter != this.selectedCounter)
                {
                    SetSelectedCounter(selectedCounter);
                }


            }
            else
                SetSelectedCounter(null);

        }
        else
            SetSelectedCounter(null);



    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = baseCounter
        });
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickedSomething(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }


    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        this.inputVector = inputVector.normalized;

    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

}
