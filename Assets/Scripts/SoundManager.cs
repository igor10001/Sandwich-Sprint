using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucces += DeliveryManager_OnRecipeSucces;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DeliveryManager_OnRecipeSucces(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
