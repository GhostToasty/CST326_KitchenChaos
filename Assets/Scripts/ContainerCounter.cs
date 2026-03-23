using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    

    //overrides the base function in BaseCounter script 
    public override void Interact(Player player)
    {
        //prevents spawning infinite kitchen items 
        if (kitchenObject == null)
        {
            //spawns kitchen object on top of counter 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            
            //tells which counter the object got spawned on
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //give object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
       
    }


    //kitchen object parent interface functions 
    public Transform GetKitchenObjectFollowTransform()
    {
        //gets new position that kitchenObject will be at 
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
