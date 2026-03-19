using Unity.Mathematics;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;


    private void Update()
    {
        //assigned the kitchen object to a different counter 
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null )
                kitchenObject.SetClearCounter(secondClearCounter);
        }
    }
    
    public void Interact()
    {
        //prevents spawning infinite kitchen items 
        if (kitchenObject == null)
        {
            //spawns kitchen object on top of counter 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            
            //tells which counter the object got spawned on
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
       
    }


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
