using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.Search;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {

        KitchenGameManagerr.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManagerr.Instance.IsCountdownStartGame())
        {
            Show();
        }
        else
            Hide();
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(KitchenGameManagerr.Instance.GetCountdownToStartTimer()).ToString();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }
}
