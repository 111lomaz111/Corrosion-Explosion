using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Search;
using UnityEngine;

public class RoundTextAnimation : MonoBehaviour
{
    public TextMeshProUGUI number;
    public GameObject roundImage;

    public Transform roundTextTarget;
    public GameObject roundImageTarget;
    void Start()
    {
        number.text = GameManager.Instance.currentRound.ToString();
        //roundTextTarget = GameObject.Find("RoundText").transform;
        roundImageTarget = GameObject.Find("RoundImage");
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 0.5f).setEaseInBounce().setOnComplete(StartAimation);
    }

    public void StartAimation()
    {
        LeanTween.scale(this.gameObject, new Vector3(0f, 0f, 0f), 0.5f).setDelay(2f).setEaseInBounce().setOnComplete(OnComplete);
    }


    void OnComplete()
    {
        Destroy(gameObject);
    }
}
