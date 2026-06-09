using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private Button button;
    private GameObject backgroundSound;
    // Start is called before the first frame update
    void Awake()
    {
        button = FindObjectOfType<Button>();
        backgroundSound = GameObject.Find("Audio");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level01()
    {
        button.ClickSound();
        Destroy(backgroundSound);
        SceneManager.LoadScene(2);
    }
    public void Level02()
    {
        button.ClickSound();
        Destroy(backgroundSound);
        SceneManager.LoadScene(3);
    }
    public void Level03()
    {
        button.ClickSound();
        Destroy(backgroundSound);
        SceneManager.LoadScene(4);
    }
    public void Back()
    {
        button.ClickSound();
        SceneManager.LoadScene(0);
    }
}
