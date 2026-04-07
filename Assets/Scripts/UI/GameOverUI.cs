using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start()
    {
        //signs up for event notifs 
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        //hides countdown UI
        Hide();
    }

    
    private void Update()
    {
        
    }
    
    
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        //shows text if the game is over, hides it otherwise
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            
            //shows recipes delivered count number as an integer
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
            Hide();
    }


    private void Show()
    {
        //shows countdown object
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        //hides countdown object 
        gameObject.SetActive(false);
    }
}
