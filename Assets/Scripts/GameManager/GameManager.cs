using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private GameState gameState;
    [SerializeField] private int money;
    [SerializeField] private int rateMoney = 10;
    [SerializeField] private float timeIncreaseMoney = 5f;
    [SerializeField] private bool isGameOver = false;
    public bool IsGameOver => isGameOver;
    public int Money => money;


    private void Start()
    {
        StartGame();
        RegisterEvent();
        
    }
    private void OnDestroy()
    {
        UnRegisterEvent();
    }
    public void StartGame()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenMenuGame>();
        }
        gameState = GameState.Start;
        StartCoroutine(IncreaseMoney());
    }

    public void SetStateGame(GameState gameState)
    {
        this.gameState = gameState;
        Debug.Log($"Game state changed to: {gameState}");
    }
    private void RegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.CHANGE_PLAYING_GAME, OnChangePlayingGame);
            ListenerManager.Instance.Register(ListenType.PLAYER_WIN, OnWin);
            ListenerManager.Instance.Register(ListenType.ZOMBIE_WIN, OnEventZombieWin);
            ListenerManager.Instance.Register(ListenType.PLANT_CREATE, OnUpdateMoney);
            ListenerManager.Instance.Register(ListenType.PLANT_CREATE_MONEY, OnIncreaseMoney);
        }
    }
    private void UnRegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.CHANGE_PLAYING_GAME, OnChangePlayingGame);
            ListenerManager.Instance.Unregister(ListenType.PLAYER_WIN, OnWin);
            ListenerManager.Instance.Unregister(ListenType.ZOMBIE_WIN, OnEventZombieWin);
            ListenerManager.Instance.Unregister(ListenType.PLANT_CREATE, OnUpdateMoney);
            ListenerManager.Instance.Unregister(ListenType.PLANT_CREATE_MONEY, OnIncreaseMoney);
        }
    }
    private void OnChangePlayingGame(object data)
    {
        StartCoroutine(DelayShowScreenSelectPlant());
    }
    private void OnWin(object data)
    {
        isGameOver = true;
        if(UIManager.HasInstance)
        {
            PopupStateGameData popupData = new()
            {
                uiState = UiState.Win,
            };
            UIManager.Instance.ShowPopup<PopupStateGame>(popupData, true);
        }
    }

    private void OnEventZombieWin(object data)
    {

        isGameOver = true;
        if (UIManager.HasInstance)
        {

            PopupStateGameData popupData = new()
            {
                uiState = UiState.Lose,

                OnLose = () =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    application.Quit();
#endif
                },
                OnReplay = () =>
                {
                    Scene currentScene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(currentScene.name);
                },
            };
            UIManager.Instance.ShowPopup<PopupStateGame>(popupData, true);
        }
    }
    private void OnUpdateMoney(object data)
    {
        if (data is TypePlant typePlant)
        {
           money -= typePlant.GetCost();
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_MONEY, money);
            }
        }
    }
    private void OnIncreaseMoney(object data)
    {
        if (data is int moneyIncrease)
        {
            money += moneyIncrease;
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_MONEY, money);
            }
        }
    }   


    IEnumerator DelayShowScreenSelectPlant()
    {
        yield return new WaitForSeconds(0.2f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenSelectPlant>();
        }
    }
    IEnumerator IncreaseMoney()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(timeIncreaseMoney);
            money += rateMoney;
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_MONEY, money);
            }
        }
        

    }    
}


