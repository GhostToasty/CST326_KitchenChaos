using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;


    private void Update()
    {
        if (isFirstUpdate)
        {
            //calls loading update only on the very first update 
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
            
        
    }
}
