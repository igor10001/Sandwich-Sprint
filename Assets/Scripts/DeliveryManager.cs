using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;


        waitingRecipeSOList= new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {

                RecipeSO waitingRecipeSO= recipeListSO.recipeListSO[Random.Range(0, recipeListSO.recipeListSO.Count)];
                Debug.Log(waitingRecipeSO.RecipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

            }
        }

    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)

        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has the same number of ingredients plate and the recipe

                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the Recipe

                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()   )
                    {
                        if(plateKitchenObject == recipeKitchenObjectSO) 
                        {
                            //Ingredients matches!
                            ingredientFound= true;
                            break;
                        }
                        //Cycling through all ingredients on the plate
                        
                    }
                    if(!ingredientFound) 
                    { 
                        //This Recipe ingredient was not found  on the Plate 
                        plateContentsMatchesRecipe = false;

                    }


                }
                if(plateContentsMatchesRecipe ) 
                {
                    //Player delivered the correct recipe!
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;

                }


            }
        }
        //no matches founddd
        //Player did not deliver the correct recipe
        Debug.Log("Player did not deliver the correct recipe");
    }



}
