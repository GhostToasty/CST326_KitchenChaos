using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    
    [SerializeField] private List<KitchenObjectSO> validkitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;
    
    
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validkitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //not a valid ingredient 
            return false;
        }
        
        //checks if item being added is a duplicate 
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //already has this type of food item
            return false;
        }
        else
        {
            //adds food item to plate in form of list 
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }


    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
