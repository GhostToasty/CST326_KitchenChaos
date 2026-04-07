using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    
    private void Awake()
    {
        Instance = this;
        
        //grabs interactions from input sheet 
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //assigns listener for when interact input is done
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        //function is called when monobehavior is destroyed 
        //unsubscribes from listeners when starting a new game; events will be re-subscribed to when new game starts 
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        //destroys input action system when starting a new game; new one will be created when new game starts
        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
