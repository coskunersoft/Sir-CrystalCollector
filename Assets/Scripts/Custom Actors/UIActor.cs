using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coskunerov.Actors;
using Coskunerov.EventBehaviour;
using Coskunerov.EventBehaviour.Attributes;

public class UIActor : GameSingleActor<UIActor>
{
    [Header("Entry")]
    public GameObject entryPanel;

    [Header("In Game Panel")]
    public GameObject inGamePanel;
    public Text inGamescoreText;

    [Header("Final Panel")]
    public GameObject finalPanel;
    public Text finalPanelScoreText;

    public void ListenButton(int id)
    {
        switch (id)
        {
            case 0:
                GameManager.Instance.StartGame();
                break;
            case 1:
                GameManager.Instance.RestartGame();
                break;
        }
    }

    [GE(BaseGameEvents.GameLoaded)]
    void OnGameLoaded()
    {
        entryPanel.SetActive(true);
    }
    [GE(BaseGameEvents.StartGame)]
    void OnStartGame()
    {
        inGamePanel.SetActive(true);
        entryPanel.SetActive(false);
    }
    [GE(BaseGameEvents.FinishGame)]
    void OnFinishGame()
    {
       
        inGamePanel.SetActive(false);
        finalPanel.SetActive(true);
    }
    [GE(BaseGameEvents.RestartGame)]
    void OnRestartGame()
    {
        finalPanel.SetActive(false);
    }
    [GE(BaseGameEvents.ScoreChanged)]
    void OnScoreChanged()
    {
        inGamescoreText.text = GameManager.Instance.runtime.Score.ToString();
        finalPanelScoreText.text = GameManager.Instance.runtime.Score.ToString();
    }
}
