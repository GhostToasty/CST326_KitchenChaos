using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions playerInputActions;

    
    private void Awake()
    {
        //grabs interactions from input sheet 
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //assigns listener for when interact input is done
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //null conditional operator to make sure value isn't null
         OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        //grabs set input values for WASD from input controller
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        //normalizes speed when player goes diagonal 
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
