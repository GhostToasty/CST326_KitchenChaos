using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;


    public KitchenObjectSO GetKitchenObjectSO()
    {
        //what type of kitchen object it is 
        return kitchenObjectSO;
    }


    //sets clearCounter when item is on it, connected to ClearCounter.cs
    public void SetClearCounter(ClearCounter clearCounter)
    {
        //refers to previous counter, being cleared 
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        
        //refers to new counter, reassigns and sets it 
        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject");
        }
        clearCounter.SetKitchenObject(this);

        //updates visual 
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }


    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
