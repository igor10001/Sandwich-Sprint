using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
   public void Interact()
    {
        Debug.Log("Interact");
        Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectSOTransform.localPosition= Vector3.zero;
    }
}
