
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private const int DEFAULT_NULL_VALUE_INT = -999;

    private bool _isEndGame;
    private int _currentScore = 0;
    private int _reversePoint = 10;
    [SerializeField] private TextMeshProUGUI _txtPoint;

    [SerializeField] private UIMainEndGame _panelEndGame;
    [SerializeField] private UIMainStartPanel _panelStart;

    [SerializeField] private BackgroundMoveControl _floor;
    [SerializeField] private CityBackgroundMovementControl _CityBackgroundMove;

    [SerializeField] private GameObject _resetPointLeft;
    [SerializeField] private GameObject _resetPointRight;

    [SerializeField] private wallMove[] _wallArray;


    private void OnEnable()
    {
        _panelEndGame.RestartGameEvent.AddListener(RestartGame);
    }
    private void OnDisable()
    {
        _panelEndGame.RestartGameEvent.RemoveListener(RestartGame);
    }
    void Start()
    {
        Time.timeScale = 0;
        _isEndGame = false;
    }

    void Update()
    {

        GetInput();
#if UNITY_EDITOR
        GetDebugInput();
#endif
    }

    private void GetDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (wallMove item in _wallArray)
            {
                item.reverseMoveSpeed();
            }
            Debug.Log("reverse");
        }
    }

    private void GetInput()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && _isEndGame == false)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _panelStart.Hide();
    }

    public void Endgame()
    {
        Time.timeScale = 0;
        _isEndGame = true;

        int hightScore = CheckHighScore();
        hightScore = hightScore == DEFAULT_NULL_VALUE_INT ? 0 : hightScore;
        _panelEndGame.ShowScore(hightScore, _currentScore);
        _panelEndGame.Show();
    }

    private int CheckHighScore()
    {
        int HightScore = PlayerPrefs.GetInt("HightScore", DEFAULT_NULL_VALUE_INT);
        if (_currentScore > HightScore)
        {
            PlayerPrefs.SetInt("HightScore", _currentScore);
        }
        return HightScore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    bool switchOn = false;
    public bool toiletGoToLeft = false;
    public void getPoint()
    {
        // game point set
        _currentScore++;
        _txtPoint.text = _currentScore.ToString();

        // stop reset wall
        if (_currentScore >= _reversePoint - 2 && switchOn == false)
        {
            if (!toiletGoToLeft)
            {
                if (_resetPointLeft.activeSelf)
                {
                    _resetPointLeft.SetActive(false);
                }
                else
                {
                    _resetPointLeft.SetActive(true);
                }

            }


            if (toiletGoToLeft)
            {
                if (_resetPointRight.activeSelf)
                {
                    _resetPointRight.SetActive(false);
                }
                else
                {
                    _resetPointRight.SetActive(true);
                }


            }


            // after this switch ON, then 
            switchOn = true;

        }


        if (_currentScore == _reversePoint)
        {
            if (toiletGoToLeft)
            {
                toiletGoToLeft = false;
            }
            else
            {
                toiletGoToLeft = true;
            }
            setIsStop(true);
            _reversePoint = _reversePoint + UnityEngine.Random.Range(5, 11);
            switchOn = false;

        }
    }

    public void setIsStop(bool value)
    {
        // set stop for all wall
        foreach (wallMove item in _wallArray)
        {
            item.isStop = value;
        }

        if (_floor.GetComponent<BackgroundMoveControl>().moveSpeed != 0)
        {
            _floor.GetComponent<BackgroundMoveControl>().moveSpeed = 0;
        }

    }

    public void ReverseWall()
    {


        foreach (wallMove item in _wallArray)
        {
            item.GetComponent<wallMove>().reverseMoveSpeed();
        }
        foreach (wallMove item in _wallArray)
        {
            _floor.GetComponent<BackgroundMoveControl>().moveSpeed = item.moveSpeed;
            _CityBackgroundMove.GetComponent<CityBackgroundMovementControl>().moveSpeed = item.moveSpeed;
            break;
        }



        if (!toiletGoToLeft)
        {
            if (_resetPointLeft.activeSelf)
            {
                _resetPointLeft.SetActive(false);
            }
            else
            {
                _resetPointLeft.SetActive(true);
            }

        }


        if (toiletGoToLeft)
        {
            if (_resetPointRight.activeSelf)
            {
                _resetPointRight.SetActive(false);
            }
            else
            {
                _resetPointRight.SetActive(true);
            }


        }
    }

    private void OnDestroy()
    {

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
