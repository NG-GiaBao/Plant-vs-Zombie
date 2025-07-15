using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
      if(UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenMenuGame>();
        }
    }
}
