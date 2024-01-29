using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _teethParent;
    [SerializeField] private Sprite endSprite;
    public int maxCorruption = 10;
    public int currentCoruption = 0;

    [SerializeField] public float respawnTime = 3f;

    public int currentRound = 0;
    public int maxMonsters = 4;
    public int monstersKilled = 0;
    public float timer = 0;
    public float delay = 0.8f;

    public bool Ending = false;

    [SerializeField] public GameObject textParent;
    [SerializeField] public GameObject endFaceAnim;
    [SerializeField] public GameObject roundParent;
    [SerializeField] public GameObject TimerText;
    [SerializeField] public GameObject CorruptionBarObject;
    [SerializeField] public GameObject TutorialPanel;

    public Image CurrentFaceSprite;
    [SerializeField] private Sprite[] faceSprites = null;

    [SerializeField] public EventReference[] roundAudioReferences;
    public int audioIndex = 0;

    private void Awake()
    {
        Instance = this;
        //nextRound();
        currentRound++;
        maxMonsters += 1;
        monstersKilled = 0;
    }

    private void Start()
    {
        maxCorruption = _teethParent.GetComponentsInChildren<Teeth>().Length * 2;
        CorruptionBar.Instance.setSliderMaxValue(maxCorruption);
        CorruptionBar.Instance.setSliderValue(0);
        CurrentFaceSprite.sprite = faceSprites[0];
    }

    public void nextRound()
    {
        currentRound++;
        if(currentRound == 2 && !Ending)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.startRound2, transform.position);
            TutorialPanel.SetActive(false);
        }
        else if (!Ending)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.startRound, transform.position);
        }

        if (audioIndex < roundAudioReferences.Length - 1)
        {
            audioIndex++;
        }
        if (respawnTime > 0.5f)
        {
            respawnTime -= 0.5f;
        }
        else
        {
            respawnTime = 0.5f;
        }
        maxMonsters += 1;
        monstersKilled = 0;
    }

    public void incrementCorruption()
    {
        //currentCoruption += 1;
        CorruptionBar.Instance.setSliderValue(++currentCoruption);

        if (currentCoruption == maxCorruption)
        {
            GameOver();
        }
        else
        {
            ChangeFaceState(currentCoruption);
        }
    }

    public void decrementCorruption()
    {
        //currentCoruption -= 1;
        //ChangeFaceState(currentCoruption);
        ChangeFaceState(--currentCoruption);
        CorruptionBar.Instance.setSliderValue(currentCoruption);
    }

    public void ChangeFaceState(int value)
    {
        float percentage = Mathf.Min(((float)value / maxCorruption) * 100);

        if (percentage >= 0 && percentage < 20)
        {
            CurrentFaceSprite.sprite = faceSprites[0];
        }
        else if (percentage >= 20 && percentage < 40)
        {
            CurrentFaceSprite.sprite = faceSprites[1];
        }
        else if (percentage >= 40 && percentage < 60)
        {
            CurrentFaceSprite.sprite = faceSprites[2];
        }
        else if (percentage >= 60 && percentage < 80)
        {
            CurrentFaceSprite.sprite = faceSprites[3];
        }
        else if (percentage >= 80 && percentage < 99)
        {
            CurrentFaceSprite.sprite = faceSprites[4];
        }
    }

    public void GameOver()
    {
        Ending = true;
        CorruptionBarObject.SetActive(false);
        roundParent.SetActive(false);
        TimerText.SetActive(false);
        endFaceAnim.SetActive(true);
        CurrentFaceSprite.gameObject.SetActive(false);
        CurrentFaceSprite.sprite = endSprite;
        CurrentFaceSprite.color = Color.black;
    }

}
