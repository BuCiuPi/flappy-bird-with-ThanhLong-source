using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletControler : MonoBehaviour
{
    public float flyPower;
    public GameObject gameController;
    public Animator anim;
    public GameObject wall;

    public float stopPosition;

    GameObject obj;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("flypower",0.0f);

        obj = gameObject;
        stopPosition = obj.transform.position.x;

        if(gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");//just like the word say
        }

        wall = GameObject.FindGameObjectWithTag("wall");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("flypower",gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // any time  click or press space_key the bird will fly up
        {
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyPower);
        }

        if (wall.GetComponent<wallMove>().isStop)
        {
                obj.transform.Translate(new Vector3(Mathf.Abs(wall.GetComponent<wallMove>().moveSpeed) * Time.deltaTime, 0, 0));//runn

            if ( Mathf.Abs(obj.transform.position.x) > 2f)
            {

                gameController.GetComponent<GameController>().setIsStop(false);
                gameController.GetComponent<GameController>().ReverseWall();

                if (obj.transform.position.x > 0)
                {
                    obj.transform.eulerAngles = new Vector3(0,180,0);
                }
                else
                {
                    obj.transform.eulerAngles = new Vector3(0, 0, 0);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isDebug)
            {
                isDebug = false;
            }else
            {
                isDebug = true;
            }
        }
    }

    bool isDebug = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDebug)
        {
            endgame();// call end game function in gameController
        }
    }

    void endgame()
    {
        gameController.GetComponent<GameController>().Endgame();// call end game function in gameController

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.GetComponent<GameController>().getPoint();
    }
}
