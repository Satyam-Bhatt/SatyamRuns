using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level03FinalButton : MonoBehaviour
{
    private Button button;
    private GameObject backgroundSound;
    private GameObject respawn;

    private void Awake()
    {
        button = FindObjectOfType<Button>();
        backgroundSound = GameObject.Find("AudioSound");
        respawn = GameObject.Find("Respawn");
       
    }
    public void LevelSelect()
    {
        button.ClickSound();
        Destroy(respawn);
        Destroy(backgroundSound);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        button.ClickSound();
        Application.Quit();
    }

}
