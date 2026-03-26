using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    //overrides the base function in BaseCounter script 
    public override void Interact(Player player)
    {
        //checks to make sure player isn't already holding a KitchenObject
        if (!player.HasKitchenObject())
        {
            //spawns stated object and makes it belong to player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            //sets off animation event 
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
           
    }

}
