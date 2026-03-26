using System;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    
    //putting a KitchenObject down on the counter 
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no KitchenObject on counter
            if (player.HasKitchenObject())
            { 
                //player is carrying something
                //checks to see if KitchenObject has a cutting recipe before letting player drop it 
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //grabs KitchenObject from player and sets parent the counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    //gets recipe for progress max and changes progress bar based on number of cuts 
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                //player not carrying anything; nothing can be put on counter
            }
        }
        else
        {
            //there is a KitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something; cannot pick up another item
                //checks if player is holding a plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //adding food item on the counter to the plate, then destroying counter item 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                //player not carrying anything
                //grabs KitchenObject from counter and sets parent to the player 
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    //cutting a KitchenObject 
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a KitchenObject on cutting board and it can be cut 
            //adds a cut and and gets the cutting recipe
            cuttingProgress++;
            
            //triggers cutting animation 
            OnCut?.Invoke(this, EventArgs.Empty);
            
            //gets cut recipe based on object put on counter 
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            //updates progress bar 
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            //lets object be cut if cuts are still left to do
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //spawns in object once player has cut enough times 
                //gets KitchenObject output from function 
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
                //cuts whole KitchenObject by destroying it and replacing it with cut KitchenObject 
                GetKitchenObject().DestroySelf();
            
                //spawns stated object and makes it belong to stated parent 
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        //checks if KitchenObject has a slicing recipe attached to it 
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        //gets the cutting recipe output (sliced item)
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.output;
        else 
            return null;
    }


    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        //gets KitchenObject output based on what's been labeled as input 
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO;
        }
        return null;
    }
}
