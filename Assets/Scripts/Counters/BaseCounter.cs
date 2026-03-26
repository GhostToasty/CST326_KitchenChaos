using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //inheritance class for all the counters; good to use since they're all similar 

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.LogError("BaseCounter.InteractAlternate();");
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
