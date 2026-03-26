using Unity.Mathematics;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    //overrides the base function in BaseCounter script 
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no KitchenObject on counter
            if (player.HasKitchenObject())
            { 
                //player is carrying something
                //grabs KitchenObject from player and sets parent the counter 
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                else
                {
                    //player is not holding a plate but something else
                    //checking to see if the counter has a plate on it 
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate 
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            //destroys food item player is holding since it's now on the plate 
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
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

}
