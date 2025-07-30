using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum AnchorPreset
{
    MidlerLeft,
    MidlerCenter,
    MidlerRight,
}

public class PopupStateGame : BasePopup
{
    [SerializeField] private TextMeshProUGUI textState;
    [SerializeField] private Button stateButton;
    [SerializeField] private RectTransform stateRect;
    [SerializeField] private RectTransform replayRect;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI stateTxt;
    [SerializeField] private TextMeshProUGUI rePlayTxt;

    private void Awake()
    {
        stateRect = stateButton.GetComponent<RectTransform>();
        replayRect = replayButton.GetComponent<RectTransform>();
    }
    private void Start()
    {
        stateButton.onClick.AddListener(OnClickButton);
    }
    public override void Show(object data)
    {
        base.Show(data);
        if (data != null)
        {
            if (data is PopupStateGameData popupData)
            {
                switch (popupData.uiState)
                {
                    case UiState.Win:
                        textState.text = popupData.winState;
                        stateTxt.text = popupData.nameWinButton;
                        replayButton.gameObject.SetActive(false);
                        SetAnchorPreset(stateRect, AnchorPreset.MidlerCenter);
                        break;
                    case UiState.Lose:
                        textState.text = popupData.loseState;
                        stateTxt.text = popupData.nameLoseButton;
                        stateButton.gameObject.SetActive(true);
                        SetAnchorPreset(stateRect, AnchorPreset.MidlerLeft);
                        stateButton.onClick.RemoveAllListeners();
                        stateButton.onClick.AddListener(() =>
                        {
                            popupData.OnLose?.Invoke();
                        });
                        replayButton.gameObject.SetActive(true);
                        SetAnchorPreset(replayRect, AnchorPreset.MidlerRight);
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

    private void SetAnchorPreset(RectTransform rt, AnchorPreset anchorPreset)
    {
        switch (anchorPreset)
        {
            case AnchorPreset.MidlerCenter:
                {
                    SetValueVector(rt, 0.5f, 0.5f);
                }
                break;
            case AnchorPreset.MidlerLeft:
                {
                    SetValueVector(rt, 0, 0.5f);
                }
                break;
            case AnchorPreset.MidlerRight:
                {
                    SetValueVector(rt, 1, 0.5f);
                }
                break;
        }

    }
    private void SetValueVector(RectTransform rt, float x, float y)
    {
        rt.anchorMin = new Vector2(x, y);
        rt.anchorMax = new Vector2(x, y);
        rt.pivot = new Vector2(x, y);
    }
    private void SetText(string textState, string statetxt,string replaytxt)
    {
        this.textState.text = textState;
    }    


}
