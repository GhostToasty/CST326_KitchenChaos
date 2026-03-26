using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        //accessing singleton Instance
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }


    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        //shows clear texture if that counter is the selected one 
        if (e.selectedCounter == baseCounter)
            Show();
        else
            Hide();
    }


    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(false);
    }

}
