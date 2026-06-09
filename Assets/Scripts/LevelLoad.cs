using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    private Button button;

    private GameObject respawn;
    // Start is called before the first frame update
    void Awake()
    {
        button = FindObjectOfType<Button>();
        respawn = GameObject.Find("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        button.ClickSound();
        Destroy(respawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
