using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        //signs up for event notifs 
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        //hides countdown UI
        Hide();
    }

    
    private void Update()
    {
        //shows countdown number as an integer
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
    
    
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        //shows text if the countdown is active, hides it otherwise
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
            Show();
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
