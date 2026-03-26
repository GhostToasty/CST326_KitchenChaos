using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        //sets recipe name 
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            //skips icon template so it isn't destroyed 
            if (child == iconTemplate) continue;
            
            //destroys all other children objects so they can be replaced 
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            //creates and reveals foot item icon
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTemplate.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
