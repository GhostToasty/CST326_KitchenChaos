using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    private void Awake()
    {
        //lambda expression, type of delegate, kinda creating a function within a function 
        
        //click
        playButton.onClick.AddListener( () =>
        {
            //changes scene to GameScene while also bringing up loading scene while waiting
            Loader.Load(Loader.Scene.GameScene);
        });

        quitButton.onClick.AddListener( () =>
        {
            //closes application; only works on built games
            Application.Quit();
            Debug.Log("quit");
        });

        //resets time scale if player gets to main menu from the pause menu
        Time.timeScale = 1f;
    }

}
