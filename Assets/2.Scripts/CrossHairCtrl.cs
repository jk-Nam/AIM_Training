using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairCtrl : MonoBehaviour
{
    private PlayerCtrl thePlayerCtrl;

    public List<GameObject> CrossHairList;

    public Text rotSpeedText;
    public Slider rotSpeedSlider;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerCtrl = FindObjectOfType<PlayerCtrl>();
        rotSpeedText = GetComponent<Text>();
        rotSpeedSlider = GetComponent<Slider>();

        //rotSpeedSlider.onValueChanged.AddListener(delegate {UpdateRotSpeed();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMouseSens()
    {
        rotSpeedText.text = "Rotation Speed" + thePlayerCtrl.rotSpeed.ToString();
    }

    //private void UpdateRotSpeed()
    //{
    //    thePlayerCtrl.rotSpeed = rotSpeedSlider.value;
    //}


}
