using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMgr : MonoBehaviour
{
    public  GameObject go_MenuUI;
    public  GameObject go_OptionUI;

    public GameObject[] crosshair;
    public Color[] crosshairColor;

    private int currnetColorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallMenu()
    {
        GameManager.isPause = true;
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
        Debug.Log("Ω√¿€");
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
                //color.a = 1.0f;
                image.color = color;
            }
        }
    }
}
