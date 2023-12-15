using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitcenObjectSO;

    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            ResetTimer();
            if(platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;
            }

        }
    }
    private void ResetTimer()
    {
        spawnPlateTimer = 0;
    }
}
