using System.Collections;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private GameState gameState;
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
        }
    }
    private void UnRegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.CHANGE_PLAYING_GAME, OnChangePlayingGame);
        }
    }
    private void OnChangePlayingGame(object data)
    {
        StartCoroutine(DelayShowScreenSelectPlant());
        //if(UIManager.HasInstance)
        //  {
        //      UIManager.Instance.ShowScreen<ScreenSelectPlant>();
        //  }
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
