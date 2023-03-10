using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMgr : MonoBehaviour
{
    private PlayerCtrl theplayerCtrl;

    public  GameObject go_MenuUI;
    public  GameObject go_OptionUI;

    public InputField rotSpeedInput;

    public GameObject[] crosshair;
    public Color[] crosshairColor;

    private int currnetColorIndex = 0;

    private const string NEWSENSIVITY = "rotSpeed";

    // Start is called before the first frame update
    void Start()
    {
        theplayerCtrl = FindObjectOfType<PlayerCtrl>();

        if (PlayerPrefs.HasKey("NEWSENSIVITY"))
        {
            theplayerCtrl.rotSpeed = PlayerPrefs.GetFloat("NEWSENSIVITY", 1.0f);
            Debug.Log("NEWSENSIVITY를 불러왔습니다.");
        }
        else
        {
            Debug.Log("저장된 값이 없습니다.");
        }
        rotSpeedInput.text = theplayerCtrl.rotSpeed.ToString();

        rotSpeedInput.onEndEdit.AddListener(ValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            CallMenu();
        }


    }

    public void ValueChanged(string text)
    {
        if (float.TryParse(rotSpeedInput.text, out float newRotSpeed))
        {
            theplayerCtrl.rotSpeed = newRotSpeed;
            PlayerPrefs.SetFloat("NEWSENSIVITY", newRotSpeed);
            Debug.Log("마우스 감도가 변경되었습니다.");
        }

    }

    public void CallMenu()
    {
        GameManager.isPause = true;
        GameManager.canMove = false;
        go_MenuUI.SetActive(true);
    }

    public void CallOptionMenu()
    {
        GameManager.isOption = true;
        go_MenuUI.SetActive(false);
        go_OptionUI.SetActive(true);
    }

    public void ClickStartButton()
    {
        go_MenuUI.SetActive(false);
        GameManager.onStart = true;
        Debug.Log("시작");
        GameManager.canMove = true;
        GameManager.isPause = false;
    }

    public void ClickOptionButton()
    {
        go_MenuUI.SetActive(false);
        go_OptionUI.SetActive(true);
        GameManager.isPause = true;
        GameManager.canMove = false;
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void ClickBack()
    {
        GameManager.isOption = false;
        go_OptionUI.SetActive(false);
        go_MenuUI.SetActive(true);
    }

    public void CrosshairChange(int index)
    {
        for (int i = 0; i < crosshair.Length; i++)
        {
            crosshair[i].gameObject.SetActive(i == index);
        }
    }

    public void CrosshairColorChange(int index)
    {
        if (index >= 0 && index < crosshairColor.Length)
        {
            currnetColorIndex = index;
            SetCrosshairColor(crosshairColor[currnetColorIndex]);
        }
    }

    private void SetCrosshairColor(Color color)
    {
        for (int i = 0; i <crosshair.Length; i++)
        {
            Image[] images = crosshair[i].GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.color = color;
            }
        }
    }

}
