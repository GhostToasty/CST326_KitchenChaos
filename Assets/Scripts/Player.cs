using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //use of property for singleton pattern
    //any class can get info, but only this class can set
    public static Player Instance {get; private set;}
    
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    //only editor can modify [SerializeField] varibale type, not public to other classes
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        //creates player instance and makes sure there's only one 
        if (Instance != null)
            Debug.LogError("More than one Player Instance");
        
        Instance = this;
    }

    
    private void Start()
    {
        //signs up for interact action notices 
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        //checks if selected counter exists and allows interaction
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }


    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking()
    {
        return isWalking;
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        if (moveDir != Vector3.zero)
            lastInteractDir = moveDir;
        
        //checks if player is standing in front of and facing counter, then selects it
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //makes counter hit by raycast the selected counter
                if (baseCounter != selectedCounter)
                    SetSelectedCounter(baseCounter);
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }


    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //make variables for values so they can be identified later and not just seen as random numbers
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        //using CapsuleCast to simulate shape of player, Raycast is too thin to detect all collisions 
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //checks if player can move diagonally/"along the wall" when they have collision with another object 
        if (!canMove)
        {
            //cannot move towards moveDir, attempt only X movement 
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can only move in X direction
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only on the X, attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move only in Z direction
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction at all 
                }
            }
        }
        
        //lets the character move when nothing is blocking it's path 
        if (canMove)
            transform.position += moveDir * moveDistance; 
        
        // set to true if the moveDirection is anything but zero
        isWalking = moveDir != Vector3.zero;

        //Slerp smooths out transition when rotating | (rotation direction, target, time/speed)
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }


    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        
        //invokes function in SelectedCounterVisuals
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs {
            selectedCounter = selectedCounter
        });
    }


    //kitchen object parent interface functions 
    //because it's an interface, the content of the function can differ, just as long as they have the same signature (name, return type, parameters)
    public Transform GetKitchenObjectFollowTransform()
    {
        //gets new position that kitchenObject will be at 
        return kitchenObjectHoldPoint;
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
