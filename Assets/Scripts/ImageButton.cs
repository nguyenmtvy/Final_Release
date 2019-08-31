using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    [SerializeField] GameObject icon;
    private Image theIcon;
    private void Start()
    {
        theIcon = icon.GetComponent<Image>();
        theIcon.enabled = true;
    }

    public void setIcon(Sprite sprite)
    {
        Debug.Log(sprite);
        theIcon.sprite = sprite;
        theIcon.enabled = true;
    }

    public void resetSprite()
    {
        theIcon.sprite = null;
        theIcon.enabled = false;
    }

    public Sprite getIcon()
    {
        return theIcon.sprite;
    }
}
