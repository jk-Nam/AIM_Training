using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Transform tr;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 400.0f;

    //void Awake()
    //{
    //    Debug.Log("Player Ready");
    //}

    //void Start()
    //{
    //    Debug.Log("hungting tool Ready");
    //}

    void Start()
    {
        tr = GetComponent<Transform>();
        Vector3 vec = new Vector3(0, 0, 0);
        new WaitForSeconds(0.3f);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        //Debug.Log("H : " + h);
        //Debug.Log("V : " + v);

        Vector3 vec = (Vector3.forward * v) + (Vector3.right * h);
        if (vec.sqrMagnitude > 1.0f) vec = vec.normalized;
        tr.Translate(vec * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }

    //void FixedUpdate()
    //{
    //    Debug.Log("Move");
    //}

    //void LateUpdate()
    //{
    //    Debug.Log("Experience increased");
    //}
}
