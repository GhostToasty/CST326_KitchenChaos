using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    //enums: fixed number of options 
    private enum Mode
    {
        LookAt, 
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;
    
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                //makes object look directly at camera 
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                //turns object to make it inverted and face opposite position  
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
