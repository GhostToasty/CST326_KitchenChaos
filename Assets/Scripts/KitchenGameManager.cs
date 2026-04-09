using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 30f;
    private bool isGamePaused = false;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }


    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        //only changes state after player input 
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }


    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            
            case State.CountdownToStart:
                //have countdown to start timer 
                countdownToStartTimer -= Time.deltaTime;
                
                //switches state when timer reaches zero and gives notif
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                //have game playing countdown timer 
                gamePlayingTimer -= Time.deltaTime;
                
                //switches state when timer reaches zero and gives notif
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }


    public bool IsGamePlaying()
    {
        //checks if game is in game playing state 
        return state == State.GamePlaying;
    }


    public bool IsCountdownToStartActive()
    {
        //checks if countdown is active 
        return state == State.CountdownToStart;
    }


    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }


    public bool IsGameOver()
    {
        return state == State.GameOver;
    }


    public float GetGamePlayingTimerNormalized()
    {
        //gets how much time has passed in the timer 
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }


    public void TogglePauseGame()
    {
        //switches the games paused state
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            //makes time scale to zero, pausing everything that uses time in it's calculations
            Time.timeScale = 0f;
            
            //shows paused screen 
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //resets the time scale to it's original state
            Time.timeScale = 1f;

            //hides paused screen 
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }

}
