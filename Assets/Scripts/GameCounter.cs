using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCounter : MonoBehaviour
{
    public static GameCounter Instance;

    [SerializeField] public float timer;
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private TextMeshProUGUI roundText;

    private void Start()
    {
        Instance = this;
        timer = 0;
        textObject.text = timer.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string niceTime = string.Format("{00:00}:{01:00}", minutes, seconds);

        textObject.text = niceTime;
        roundText.text = GameManager.Instance.currentRound.ToString();
    }
}
