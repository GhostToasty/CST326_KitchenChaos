using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plates
                //checks plate contents with waiting recipes 
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                
                //destroys plate and everything on it when put on the counter
                player.GetKitchenObject().DestroySelf();
            }
            
        }
    }
}
