using System;
using UnityEngine;

public class PopupStateGameData
{
    public UiState uiState;
    public string winState = "WIN";
    public string loseState = "LOSE";
    public string nameWinButton = "Continue";
    public string nameLoseButton = "Quit";
    public string nameReplayButton = "RePlay";
    public Action OnWin;
    public Action OnLose;
    public Action OnReplay;

}
