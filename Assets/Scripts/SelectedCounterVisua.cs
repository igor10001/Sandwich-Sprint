using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisua : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
        {
            Debug.Log("visual: " + clearCounter);
            Show();
            Debug.Log(clearCounter.gameObject.name);
        }
    }


    private void Show()
    {
        visualGameObject.SetActive(true);

    }

    private void Hide()
    {
        visualGameObject.SetActive(false);

    }

   
}
