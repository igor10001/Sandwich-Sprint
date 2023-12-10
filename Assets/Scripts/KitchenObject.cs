using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    //return scriptable instance of food
    public KitchenObjectSO GetKitchenObjectSO()
    { 
        return kitchenObjectSO;
    }
     
    //make kitchen object to know on whan conter is placed
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero; 
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
