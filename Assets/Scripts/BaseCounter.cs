using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    //inheritance class for all the counters; good to use since they're all similar 

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

}
