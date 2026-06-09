using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = FindObjectOfType<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        button.ClickSound();
        Application.Quit();
    }

    public void Play()
    {
        button.ClickSound();
        SceneManager.LoadScene(1);        
    }

    public void About()
    {
        button.ClickSound();
        SceneManager.LoadScene(5);        
    }
}
