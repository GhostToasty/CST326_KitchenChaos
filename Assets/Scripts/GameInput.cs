using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
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
