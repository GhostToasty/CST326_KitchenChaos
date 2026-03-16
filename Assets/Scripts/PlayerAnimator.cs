using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    //variable for string to catch compiler error
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player player;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
