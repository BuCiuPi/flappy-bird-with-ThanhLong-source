using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainEndGame : UIPanel
{
    [SerializeField] private TextMeshProUGUI TxtNewScore;
    [SerializeField] private TextMeshProUGUI TxtHightScore;

    public void ShowNewScore(int score)
    {
        TxtNewScore.text = score.ToString();
    }

    public void ShowHighScore(int score)
    {
        TxtHightScore.text = score.ToString();
    }
}
