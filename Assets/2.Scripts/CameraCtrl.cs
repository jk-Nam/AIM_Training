using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform targetTr;

    private Transform camTr;


    //public float damping = 0.1f;
    public float targetOffset = 1.9f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return new WaitForSeconds(1.0f);
    }

    void update()
    {


    }
}
