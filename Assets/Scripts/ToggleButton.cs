using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Color Active;
    public Color notActive;
    private Image button;
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame

    public void SwitchOnOff()
    {
        if (!isActive)
        {
            button.color = Active;
            isActive = true;
        }
        else
        {
            isActive = false;
            button.color = notActive;
        }

    }
}
