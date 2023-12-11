using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kichen object on the counter
            if (player.HasKitchenObject())
            {
                //player have carrying object 
                player.GetKitchenObject().SetKitchenObjectParent(this);

            }
            else
            {
                //player not carrying anything
            }

        }
        else
        {
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

    public override void InteractAlternate(Player player)
    {
       if(HasKitchenObject())
        {
            //There is a kitchenObject here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
