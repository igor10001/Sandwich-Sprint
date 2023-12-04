using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;



    private Vector3 lastInteractedDirection;

    private bool isWalking;
    private void Update()
    {

        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    { return isWalking; }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

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

        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    private void HandleInteractions()
    {


        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir!= Vector3.zero)
        {
            lastInteractedDirection= moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractedDirection, out RaycastHit raycastHit, interactDistance))
            Debug.Log(raycastHit.transform);

    }

}
