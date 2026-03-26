using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;


    private void Awake()
    {
        //hides template
        recipeTemplate.gameObject.SetActive(false);
    }


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    
        UpdateVisual();
    }
    
    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            //prevents recipe template from being destroyed
            if (child == recipeTemplate) continue;

            //destroys all other chilren inside 
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            //creates a copy of the recipe template 
            Transform recipeTransform = Instantiate(recipeTemplate, container);

            //makes component visible 
            recipeTransform.gameObject.SetActive(true);

            //calls for recipe name to be printed on the UI component 
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
