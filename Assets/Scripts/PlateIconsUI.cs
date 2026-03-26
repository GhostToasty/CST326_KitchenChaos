using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        //icon template is invisible
        iconTemplate.gameObject.SetActive(false);
    }
    

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        //updates visual when a food item is added to the plate 
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        //goes through all icons and destroys them
        foreach (Transform child in transform)
        {
            //skips iconTemplate so it won't be destroyed
            if (child == iconTemplate) continue;
            
            //destroys all other icon child objects 
            Destroy(child.gameObject);
        }

        //adds all icons back as list is parsed
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            //spawns icon as a child object and reveals icon
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            
            //gets icon from KitchenObjectSO and sets it
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
