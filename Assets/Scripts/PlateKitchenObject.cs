using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSO;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList= new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSO.Contains(kitchenObjectSO))
            
            //Not a valid ingredient
            return false;
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {

            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO

            }) ;

            return true;

        }
        
    }
}
