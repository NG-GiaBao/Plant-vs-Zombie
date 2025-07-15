using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupStateGame : BasePopup
{
    [SerializeField] private TextMeshProUGUI textState;
    [SerializeField] private Button Button;
    [SerializeField] private TextMeshProUGUI textButton;

    private void Start()
    {
        Button.onClick.AddListener(OnClickButton);
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
                        textButton.text = popupData.nameWinButton;
                        break;
                    case UiState.Lose:
                        textState.text = popupData.loseState;
                        textButton.text = popupData.nameLoseButton;
                        Button.onClick.RemoveAllListeners();
                        Button.onClick.AddListener(() => 
                        {
                            popupData.OnLose?.Invoke();
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
