using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public bool IsRunning = false;
    private float timeRemaining = 0;
    private float timeBetweenRounds = 10;

    public bool roundBreak = false;

    public GameObject RoundTextPrefab;
    public Transform CanvasParent;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (IsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if(timeBetweenRounds > 0 && timeBetweenRounds > 2)
            {
                timeBetweenRounds -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                if (roundBreak && !GameManager.Instance.Ending)
                {
                    AudioManager.Instance.PlayOneShot(GameManager.Instance.roundAudioReferences[GameManager.Instance.audioIndex], this.transform.position);
                    roundBreak = false;
                }
                IsRunning = false;
                timeBetweenRounds = 0;
                MonstersLogic.Instance.RespawnLogic();
            }
        }
    }

    public void setTime(float value)
    {
        timeRemaining = value;
        IsRunning = true;
    }

    public void startRound()
    {
        if (!GameManager.Instance.Ending)
        {
            timeBetweenRounds = 10f;
            IsRunning = true;
            Vector2 spawnPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.8f));
            Instantiate(RoundTextPrefab, spawnPos, Quaternion.identity, CanvasParent);
            roundBreak = true;
        }
    }

    public void startFirstRound()
    {
        if(!GameManager.Instance.Ending)
        {
            timeBetweenRounds = 3f;
            IsRunning = true;
            Vector2 spawnPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.8f));
            Instantiate(RoundTextPrefab, spawnPos, Quaternion.identity, CanvasParent);
        }
    }
}
