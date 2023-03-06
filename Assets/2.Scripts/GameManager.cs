using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private UIMgr theUIMgr;

    public List<Transform> points = new List<Transform>();
    public List<GameObject> targetList = new List<GameObject>();
    public int currentTargetCount = 1;
    public int maxTargetCount = 10;
    public GameObject go_target;
    public float createTime = 0.1f;
    public Text scoreText;
    public Text timeUI;

    private GunCtrl theGunctrl;

    private RaycastHit hit;

    public float setTime = 60.0f;

    private int totScore = 0;

    public static bool onStart = false;
    public static bool isPause = false;
    public static bool isOption = false;
    public static bool canMove = false;

    private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateTarget");
            }
        }
    }

    public delegate void GameOverHandler();
    public static event GameOverHandler OnTimeOver;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        theUIMgr = FindObjectOfType<UIMgr>();
        theGunctrl = FindObjectOfType<GunCtrl>();
        
        Transform targetRespawnGroup = GameObject.Find("TargetRespawnGroup").transform;

        foreach (Transform point in targetRespawnGroup)
        {
            points.Add(point);
        }
        InvokeRepeating("CreateTarget", 0.1f, createTime);
        CreateTargetPool();

        //totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        totScore = 0;
        DisplayScore(0);

        theUIMgr.CallMenu();
    }

    private void Update()
    {
        if (onStart == true && !isPause)
        {
            setTime -= Time.deltaTime;

            if (setTime <= 60.0f)
            {
                timeUI.text = "남은 시간 : " + Mathf.Floor(setTime).ToString() + "" + "초";
            }

            if (setTime <= 0.0f)
            {
                timeUI.text = "남은 시간 : 0초";
                TimeOver();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(theGunctrl.raycastStartSpot.position, theGunctrl.raycastStartSpot.forward, out hit, 20.0f))
                {
                    Debug.Log($"Hit = {hit.transform.name}");
                    if (hit.collider.gameObject.tag == "Target")
                    {
                        Debug.Log("명중");
                        hit.transform.gameObject.SetActive(false);
                        instance.DisplayScore(10);

                        currentTargetCount++;

                        if (currentTargetCount > targetList.Count)
                        {
                            var newTarget = Instantiate<GameObject>(go_target);
                            newTarget.name = $"Target_{targetList.Count:00}";
                            newTarget.SetActive(false);
                            targetList.Add(newTarget);
                        }
                    }
                }
            }
        }
    }

    void CreateTargetPool()
    {
        foreach (var _target in targetList)
        {
            Destroy(_target);
        }
        targetList.Clear();

        for (int i = 0; i < currentTargetCount; i++)
        {
            var _target = Instantiate<GameObject>(go_target);
            _target.name = $"Target_{i:00}";
            _target.SetActive(false);
            targetList.Add(_target);
        }
    }

    void CreateTarget()
    {
            if (currentTargetCount >= maxTargetCount)
            {
                currentTargetCount = maxTargetCount;
            }

            int idx = Random.Range(0, points.Count);

            Collider[] coll = Physics.OverlapSphere(points[idx].position, 0.5f);
            if (coll.Length > 0)
            {
                idx = (idx + 1) % points.Count;
            }

            GameObject _target = GetTargetPool();
            _target?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
            _target?.SetActive(true);
                     
    }


    public GameObject GetTargetPool()
    {
        foreach (var _target in targetList)
        {
            if (_target.activeSelf == false)
            {
                return _target;
            }
        }
        return null;
    }

    

    public void DisplayScore(int score)
    {
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE : </color><color=#ff0000>{totScore:#,##0}</color>";
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

    void TimeOver()
    {
        Debug.Log("Time Over!");

        OnTimeOver();
        IsGameOver = true;
    }
}
