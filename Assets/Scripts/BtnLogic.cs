using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BtnLogic : MonoBehaviour
{
    public Sprite buttonSprite;
    public Sprite highlightSprite;
    public Sprite sprite;
    public Image buttonImage;

    public void clicked()
    {
        buttonImage.sprite = buttonSprite;
        BackgroundMusic.Music.setParameterByName("Parameter 1", 1);
    }

    void OnMouseOver()
    {
        transform.GetComponent<Image>().sprite = highlightSprite;
    }

    void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = sprite;
    }
}
