using TMPro;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;
using UnityEngine.UI;

public class PopupStateGame : BasePopup
{
    [SerializeField] private TextMeshProUGUI textState;
    [SerializeField] private Button stateButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI stateTxt;
    [SerializeField] private TextMeshProUGUI rePlayTxt;

    private void Start()
    {
        stateButton.onClick.AddListener(OnClickButton);
    }
    public override void Show(object data)
    {
        base.Show(data);
        if(data != null)
        {
            if(data is PopupStateGameData popupData)
            {
               switch(popupData.uiState)
                {
                    case UiState.Win:
                        textState.text = popupData.winState;
                        stateTxt.text = popupData.nameWinButton;
                        replayButton.gameObject.SetActive(false);
                        break;
                    case UiState.Lose:
                        textState.text = popupData.loseState;
                        stateTxt.text = popupData.nameLoseButton;
                        stateButton.onClick.RemoveAllListeners();
                        stateButton.onClick.AddListener(() => 
                        {
                            popupData.OnLose?.Invoke();
                        });
                        replayButton.gameObject.SetActive(true);
                        rePlayTxt.text = popupData.nameReplayButton;
                        replayButton.onClick.RemoveAllListeners();
                        replayButton.onClick.AddListener(() => 
                        {
                            popupData.OnReplay?.Invoke();
                            this.Hide();
                            GameManager.Instance.SetStateGame(GameState.Playing);

                        });
                      
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnClickButton()
    {
        this.Hide();
    }    


}
