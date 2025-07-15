using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private GameState gameState;
    [SerializeField] private int money;
    [SerializeField] private int ZombieDeadCount = 0;
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
            ListenerManager.Instance.Register(ListenType.ZOMBIE_DEAD, OnEventZombieDead);
            ListenerManager.Instance.Register(ListenType.ZOMBIE_WIN, OnEventZombieWin);
            ListenerManager.Instance.Register(ListenType.PLANT_CREATE, OnUpdateMoney);
        }
    }
    private void UnRegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.CHANGE_PLAYING_GAME, OnChangePlayingGame);
            ListenerManager.Instance.Unregister(ListenType.ZOMBIE_DEAD, OnEventZombieDead);
            ListenerManager.Instance.Unregister(ListenType.ZOMBIE_WIN, OnEventZombieWin);
            ListenerManager.Instance.Unregister(ListenType.PLANT_CREATE, OnUpdateMoney);
        }
    }
    private void OnChangePlayingGame(object data)
    {
        StartCoroutine(DelayShowScreenSelectPlant());
    }
    private void OnEventZombieDead(object data)
    {
        if (data is ZombieType zombieType)
        {
            if (zombieType == ZombieType.ZOMBIE_NORMAL)
            {
                ZombieDeadCount++;
                int amoutZombie = SpawnController.Instance.AmountZombie;
                if (ZombieDeadCount >= amoutZombie)
                {
                    if (UIManager.HasInstance)
                    {
                        PopupStateGameData popupData = new()
                        {
                            uiState = UiState.Win,
                            OnWin = () =>
                            {

                            },
                        };
                        UIManager.Instance.ShowPopup<PopupStateGame>(popupData, true);
                    }
                }
            }
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


    IEnumerator DelayShowScreenSelectPlant()
    {
        yield return new WaitForSeconds(0.2f);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenSelectPlant>();
        }
    }
}
