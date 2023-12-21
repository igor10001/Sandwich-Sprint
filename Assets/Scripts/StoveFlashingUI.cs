using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFlashingUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private readonly string IS_Flashing = "IsFlashing";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized > burnShowProgressAmount;

        animator.SetBool(IS_Flashing, show);
    }









}
