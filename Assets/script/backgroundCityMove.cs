using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundCityMove : MonoBehaviour
{
    private GameObject obj;

    public float moveSpeed = 1.0f;
    public float backgroundRange = 2.18f;

    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        oldPosition = obj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.Translate(new Vector3((-moveSpeed/5) * Time.deltaTime, 0, obj.transform.position.z)); //move the background , change by movespeed

        if (Mathf.Abs(Vector3.Distance(oldPosition, obj.transform.position)) > backgroundRange) //create a infinity background
        {
            obj.transform.position = oldPosition;
        }
    }

    public void reverseMoveSpeed()
    {
        moveSpeed *= -1f;
    }
}
