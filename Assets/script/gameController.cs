
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    bool isEndGame;
    int gamePoint = 0;
    public int reversePoint = 10;
    public TextMeshPro txtPoint;

    public GameObject panelEndGame;
    public TextMeshProUGUI txtPanelPoint;
    public GameObject resetPointLeft;
    public GameObject resetPointRight;
    public GameObject tutorial;
    public GameObject backgroundCity;
    public List<GameObject> wall;

    GameObject[] wallArray;
    GameObject floor;
    


    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 0;//"stop it , don't fly away fukking eye !! "
        isEndGame = false;
        wallArray = GameObject.FindGameObjectsWithTag("wall");
        floor = GameObject.FindGameObjectWithTag("floor");
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isEndGame == false)
        {
            Time.timeScale = 1;
            tutorial.SetActive(false);
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
        Time.timeScale = 0;//stop the fukking game
        Debug.Log("EndGame");
        isEndGame = true;
        txtPanelPoint.text = "Score: " + gamePoint.ToString();
        panelEndGame.SetActive(true);
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
        gamePoint++;
        txtPoint.text = gamePoint.ToString();

        // stop reset wall
         if (gamePoint >= reversePoint - 2 && switchOn == false)
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
                if (resetPointRight.activeSelf )
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

        
        if (gamePoint == reversePoint)
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
            reversePoint = reversePoint + Random.Range(5, 11);
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
        if (floor.GetComponent<backgroundMove>().moveSpeed != 0)
        {
            floor.GetComponent<backgroundMove>().moveSpeed = 0;
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
            floor.GetComponent<backgroundMove>().moveSpeed = item.GetComponent<wallMove>().moveSpeed;
            backgroundCity.GetComponent<backgroundCityMove>().moveSpeed = item.GetComponent<wallMove>().moveSpeed;
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
