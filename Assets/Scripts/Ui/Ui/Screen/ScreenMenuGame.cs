using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenMenuGame : BaseScreen
{
    [SerializeField] private Button startGameBtn;
    [SerializeField] private string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startGameBtn.onClick.AddListener(OnClickStartGameBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClickStartGameBtn()
    {
        SceneManager.LoadScene(sceneName);
        if(GameManager.HasInstance)
        {
            GameManager.Instance.SetStateGame(GameState.Playing);
        }
        if(ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.CHANGE_PLAYING_GAME,null);
        }
        if(UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHealHouse>();
        }
        this.Hide();
    }
}
