using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

   public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kichen object on the counter
            if(player.HasKitchenObject())
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
            if(player.HasKitchenObject())
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


}
