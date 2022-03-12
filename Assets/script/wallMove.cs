using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMove : MonoBehaviour
{
    public GameObject floor;
    public float moveSpeed = 2;
    float resetPoint = 7;
    public float min = -1.5f;
    public float max = 1.5f;
    private GameObject obj;
    public bool isStop;
        
        // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;



        isStop = false;
        floor = GameObject.FindGameObjectWithTag("floor");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            obj.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));//runn
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            obj.transform.position = new Vector3(resetPoint, Random.Range(min, max+1),0); // make the wall come back after it run out of the camera
        }
        
    }

    public void reverseMoveSpeed()
    {
        moveSpeed = -moveSpeed;
        resetPoint = -resetPoint;
    }
}
