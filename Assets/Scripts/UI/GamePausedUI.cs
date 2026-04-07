using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            //turns off paused screen to return to game 
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            //brings up load scene and then brings player to main menu
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }


    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }


    private void Show()
    {
        //shows paused menu
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        //hides paused menu 
        gameObject.SetActive(false);
    }

}
