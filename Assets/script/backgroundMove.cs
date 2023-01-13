using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float moveSpeed = 3;

    private Vector3 oldPosition;
    float backgroundRange;

    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        oldPosition = obj.transform.position;
        backgroundRange = 4f;

    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime,0, obj.transform.position.z)); //move the background , change by movespeed
        
        if(Mathf.Abs(Vector3.Distance(oldPosition, obj.transform.position)) > backgroundRange) //create a infinity background
        {
            obj.transform.position = oldPosition;
        }
    }
}
