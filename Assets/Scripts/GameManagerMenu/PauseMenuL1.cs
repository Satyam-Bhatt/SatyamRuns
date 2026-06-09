using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuL1 : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Canvas canvas;

    private GameObject respawn;
    private Button button;
    private GameObject backgroundSound;

    [HideInInspector]
    public bool resumeClicked = false;
    // Start is called before the first frame update
    void Awake()
    {
        button = FindObjectOfType<Button>();
        respawn = GameObject.Find("Respawn");
        backgroundSound = GameObject.Find("AudioSound");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        button.ClickSound();
        pauseMenu.SetActive(false);
        resumeClicked = true;
        canvas.enabled = true;
    }

    public void Level01()
    {
        SceneManager.LoadScene(2);
        Destroy(respawn);
        button.ClickSound();
        canvas.enabled = true;
    }

    public void Level02()
    {
        SceneManager.LoadScene(3);
        Destroy(respawn);
        button.ClickSound();
        canvas.enabled = true;
    }

    public void Level03()
    {
        SceneManager.LoadScene(4);
        Destroy(respawn);
        button.ClickSound();
        canvas.enabled = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Destroy(respawn);
        button.ClickSound();
        Destroy(backgroundSound);
        canvas.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
