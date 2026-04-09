using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter; 


    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //checks if progress is over certain amount and if meat is in fried state 
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.isFried() && e.progressNormalized >= burnShowProgressAmount;

        //will show warning above statement is true
        if (show)
            Show();
        else 
            Hide();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
