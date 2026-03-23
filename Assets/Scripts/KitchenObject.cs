using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;


    public KitchenObjectSO GetKitchenObjectSO()
    {
        //what type of kitchen object it is 
        return kitchenObjectSO;
    }


    //sets clearCounter when item is on it, connected to ClearCounter.cs
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //refers to previous counter, being cleared 
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        //refers to new counter, reassigns and sets it 
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);

        //updates visual 
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }


    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
