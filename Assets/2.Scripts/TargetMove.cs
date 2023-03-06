using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    private Transform targetTr;
    Vector3 target = new Vector3(6, 0, 9);
    float r = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -6.0f)
            r = 1.0f;
        else if (transform.position.x >= 6.0f)
            r = -1.0f;
        transform.Translate(Vector3.right * 5.0f * Time.deltaTime * r);
    }
}
