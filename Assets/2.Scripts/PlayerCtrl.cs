using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{    
    public Transform playerTr;

    public  float rotSpeed = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        //ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.canMove == true)
        {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-v, h, 0);

        Vector3 angle = transform.eulerAngles;
        angle += dir * rotSpeed * 1000 * Time.deltaTime;
        transform.eulerAngles = angle;
        }
    }

}
