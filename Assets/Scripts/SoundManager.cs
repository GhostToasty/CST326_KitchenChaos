using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    
    public static SoundManager Instance { get; private set; } //singleton
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;
    

    private void Awake()
    {
        Instance = this;

        //setting default value 
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    

    private void Start()
    {
        //listening to events 
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    } 
    
    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        //sets audio position to base counter and randomzies pick up sound 
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }
    
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        //sets audio position to base counter and randomzies pick up sound 
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }
    
    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        //sets audio position to player and randomzies pick up sound 
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    } 
    
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        //sets audio position to cutting counter and randomzies cutting sound 
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }
    
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        //sets audio position to delivery counter and plays failed sound 
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }
    
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        //sets audio position to delivery counter and plays success sound 
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        //randomizes from an array of audio clips and plays it
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        //plays single audio clip with no randomization 
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }


    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        //will set player as position and allows adjustable volume
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        //will play every time a new number appears during the countdown 
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }


    public void PlayWarningSound(Vector3 position)
    {
        //will play every time a new number appears during the countdown 
        PlaySound(audioClipRefsSO.warning, position);
    }


    public void ChangeVolume()
    {
        //increases volume by 10%
        volume += 0.1f;

        //resets volume back to zero when volume goes past max 
        if (volume > 1f)
            volume = 0;

        //saves set volume data even after the game has be exited 
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }


    public float GetVolume()
    {
        return volume;
    }
}
