using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtrl : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    private void OnEnable()
    {
        GameManager.OnTimeOver += this.TimeOver;
    }

    private void OnDisable()
    {
        GameManager.OnTimeOver -= this.TimeOver;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RemoveTarget()
    {
        
        {
            GetComponent<SphereCollider>().enabled = false;
        
            this.gameObject.SetActive(false);

        }
    }

    void TimeOver()
    {
        StopAllCoroutines();        
    }
}
