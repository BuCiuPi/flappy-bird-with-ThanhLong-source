using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainEndGame : UIPanel
{
    [Space]
    [SerializeField] private TextMeshProUGUI _txtNewScore;
    [SerializeField] private TextMeshProUGUI _txtHightScore;
    [SerializeField] private GameObject _congratulationBanner;

    [Header("Button")]
    [SerializeField] private Button _restartButton;

    public UnityEvent RestartGameEvent;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartGame);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartGame);
    }

    private void OnRestartGame()
    {
        RestartGameEvent.Invoke();
    }

    private void ShowNewScore(int score)
    {
        _txtNewScore.text = score.ToString();
    }

    private void ShowHighScore(int score)
    {
        _txtHightScore.text = score.ToString();
    }

    public void ShowScore(int hightScore, int currentScore)
    {
        ShowHighScore(hightScore);
        ShowNewScore(currentScore);
        _congratulationBanner.SetActive(currentScore > hightScore);
    }


}
