using System.Threading;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.18f;
    
    private void Awake()
    {
        player = GetComponent<Player>();
    }


    private void Update()
    {
        //keeps track of how often footstep sound is allowed to be played to give intermittence
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
 
            //checks if player is moving before playing sound 
            if (player.IsWalking())
            {
                //allows volume to be adjusted
                float volume = 1f;

                //plays footsteps sound 
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }  
    }

}
