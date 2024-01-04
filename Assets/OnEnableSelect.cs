using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OnEnableSelect : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void OnEnable()
    {
        btn = GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(btn.gameObject);
    }

}
