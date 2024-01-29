using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndAnimFunction : MonoBehaviour
{
    public EventReference lough;
    public void AnimationEndFunction()
    {
        GameManager.Instance.CurrentFaceSprite.gameObject.SetActive(true);
        GameManager.Instance.textParent.SetActive(true);
        GameManager.Instance.endFaceAnim.SetActive(false);
    }

    public void PlaySound()
    {
        //AudioManager.Instance.PlayOneShot(GameManager.Instance.roundAudioReferences[GameManager.Instance.audioIndex], this.transform.position);
        BackgroundMusic.EndGame.start();
    }
}
