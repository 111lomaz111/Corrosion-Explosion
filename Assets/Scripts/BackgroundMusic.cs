using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    public static FMOD.Studio.EventInstance Music;
    public static FMOD.Studio.EventInstance EndGame;

    public EventReference music;

    private void Awake()
    {
        Instance = this;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/MUZA");
        Music.start();

        EndGame = FMODUnity.RuntimeManager.CreateInstance("event:/end game");
        //Atmo = FMODUnity.RuntimeManager.CreateInstance("event:/atmo");
    }
    //public void PlayOneShot()
    //{
    //    BackgroundMusic.EndGame.start();
    //    BackgroundMusic.EndGame.setVolume(100f);
    //}
}
