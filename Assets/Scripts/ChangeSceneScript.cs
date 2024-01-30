using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    public GameObject transitionPanel;

    public void ChangeScene()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ChangeSceneMainMenu()
    {
        StartCoroutine(LoadLevel(0));
        BackgroundMusic.Music.setParameterByName("Parameter 1", 0);
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transitionPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    public void DestroyOnEnd()
    {
        Destroy(this.gameObject);
    }
}
