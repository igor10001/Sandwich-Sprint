using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;

    private enum State
    {
        Idle,
        Frying, 
        Fried, 
        Burned,
    }

    private State state;

    private void Update()
    {
        if(HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        Debug.Log("Fried");
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    if (burningTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        Debug.Log("Fried");
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Burned;
                        burningTimer = 0f;
                    }
                    break;  
                case State.Burned:
                    break;


            }
            Debug.Log(state );

        }


       

    }
    public override void Interact(Player player)
    {
        Debug.Log("interact cut");
        if (!HasKitchenObject())
        {

            // no kichen object on the counter
            if (player.HasKitchenObject())
            {
                //player have carrying object 
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be cut 
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                     fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                }
                else
                {
                    //player not carrying anything
                }

            }


        }
        else
        {
            Debug.Log("debug: " + player.HasKitchenObject());
            // have kitchen object 
            if (player.HasKitchenObject())
            {
                //Player is carrying something 
            }
            else
            {

                //Player is not carrying anything 
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }


    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
}
