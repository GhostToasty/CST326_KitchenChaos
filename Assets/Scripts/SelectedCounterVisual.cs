using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        //accessing singleton Instance
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }


    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        //shows clear texture if that counter is the selected one 
        if (e.selectedCounter == clearCounter)
            Show();
        else
            Hide();
    }


    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }

}
