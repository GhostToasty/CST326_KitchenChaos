using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    
    public static MusicManager Instance { get; private set; }
    
    private AudioSource audioSource;
    private float volume = 0.3f; 

    
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        
        //setting default value
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume; 
    }
    
    public void ChangeVolume()
    {
        //increases volume by 10%
        volume += 0.1f;

        //resets volume back to zero when volume goes past max 
        if (volume > 1f)
            volume = 0;

        //updates volume 
        audioSource.volume = volume; 
        
        //saves set volume data even after the game has be exited  
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }


    public float GetVolume()
    {
        return volume;
    }
}
