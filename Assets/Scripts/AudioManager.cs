using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public EventReference enemySound;
    public EventReference corruptTeeth;
    public EventReference destroyTeeth;
    public EventReference jumpTeeth;
    public EventReference moveSound;
    public EventReference cleaningSound;
    public EventReference startRound;
    public EventReference startRound2;
    public EventReference ambient;

    private void Awake()
    {
        Instance = this;
        PlayOneShot(ambient, transform.position);
        //BackgroundMusic.Atmo.start();
        //BackgroundMusic.Atmo.setVolume(1f);
    }
    public void PlayOneShot(EventReference eventReference, Vector3 worldpos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldpos);
    }
}
