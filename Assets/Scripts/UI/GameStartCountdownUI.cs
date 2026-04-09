using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber; 


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    

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
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber)
        {
            //sets new countdown number and plays sound 
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
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
