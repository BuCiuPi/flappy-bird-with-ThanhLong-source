
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    private const int DEFAULT_NULL_VALUE_INT = -999;

    bool isEndGame;
    private int _currentScore = 0;
    public int reversePoint = 10;
    public TextMeshProUGUI txtPoint;

    [SerializeField] private UIMainEndGame _panelEndGame;
    [SerializeField] private UIMainStartPanel _panelStart;

    [SerializeField] private BackgroundMove _floor;
    [SerializeField] private BackgroundCityMove backgroundCity;

    public GameObject resetPointLeft;
    public GameObject resetPointRight;
    

    public List<GameObject> wall;

    GameObject[] wallArray;




    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        isEndGame = false;
        wallArray = GameObject.FindGameObjectsWithTag("wall");
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isEndGame == false)
        {
            Time.timeScale = 1;
            _panelStart.Hide();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject item in wallArray)
            {
                item.GetComponent<wallMove>().reverseMoveSpeed();

            }
            Debug.Log("reverse");
        }
    }

    public void Endgame()
    {
        Time.timeScale = 0;
        isEndGame = true;

        int HightScore = CheckHighScore();
        HightScore = HightScore == DEFAULT_NULL_VALUE_INT ? 0 : HightScore;
        _panelEndGame.ShowHighScore(HightScore);
        _panelEndGame.ShowNewScore(_currentScore);
        _panelEndGame.Show();
    }

    private int CheckHighScore()
    {
        int HightScore = PlayerPrefs.GetInt("HightScore", DEFAULT_NULL_VALUE_INT);
        if (HightScore == DEFAULT_NULL_VALUE_INT)
        {
            if (_currentScore > HightScore)
            {
                PlayerPrefs.SetInt("HightScore", _currentScore);

            }
            return HightScore;
        }

        return DEFAULT_NULL_VALUE_INT;
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    bool switchOn = false;
    public bool toiletGoToLeft = false;
    public void getPoint()
    {
        // game point set
        _currentScore++;
        txtPoint.text = _currentScore.ToString();

        // stop reset wall
        if (_currentScore >= reversePoint - 2 && switchOn == false)
        {
            if (!toiletGoToLeft)
            {
                if (resetPointLeft.activeSelf)
                {
                    resetPointLeft.SetActive(false);
                }
                else
                {
                    resetPointLeft.SetActive(true);
                }

            }


            if (toiletGoToLeft)
            {
                if (resetPointRight.activeSelf)
                {
                    resetPointRight.SetActive(false);
                }
                else
                {
                    resetPointRight.SetActive(true);
                }


            }


            // after this switch ON, then 
            switchOn = true;

            Debug.Log("turn off wall respawn");
        }


        if (_currentScore == reversePoint)
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
            reversePoint = reversePoint + UnityEngine.Random.Range(5, 11);
            switchOn = false;

        }
    }

    public void setIsStop(bool value)
    {
        // set stop for all wall
        foreach (GameObject item in wallArray)
        {
            item.GetComponent<wallMove>().isStop = value;
            Debug.Log("wall is stop");
        }
        if (_floor.GetComponent<BackgroundMove>().moveSpeed != 0)
        {
            _floor.GetComponent<BackgroundMove>().moveSpeed = 0;
        }

    }

    public void ReverseWall()
    {


        foreach (GameObject item in wallArray)
        {
            item.GetComponent<wallMove>().reverseMoveSpeed();
        }
        foreach (GameObject item in wallArray)
        {
            _floor.GetComponent<BackgroundMove>().moveSpeed = item.GetComponent<wallMove>().moveSpeed;
            backgroundCity.GetComponent<BackgroundCityMove>().moveSpeed = item.GetComponent<wallMove>().moveSpeed;
            break;
        }



        if (!toiletGoToLeft)
        {
            if (resetPointLeft.activeSelf)
            {
                resetPointLeft.SetActive(false);
            }
            else
            {
                resetPointLeft.SetActive(true);
            }

        }


        if (toiletGoToLeft)
        {
            if (resetPointRight.activeSelf)
            {
                resetPointRight.SetActive(false);
            }
            else
            {
                resetPointRight.SetActive(true);
            }


        }
    }

}
