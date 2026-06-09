using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    public GameObject infoPanel;

    [HideInInspector]
    public bool gameIsPause = false;
    [HideInInspector]
    public bool moveCome = false;
    [HideInInspector]
    public bool finish = false;
    [HideInInspector]
    public bool died;

    private Respawn respawn;
    private Button button;
    private GameObject hintPanel;
    private PauseMenuL1 pauseMenuL1;

    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject rewindText;
    [SerializeField] private GameObject startScreenCamera;
    [SerializeField] private GameObject gameplayCamera;
    [SerializeField] private PlayerMovementController player;
    [SerializeField] private GameObject finishUI;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource death;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject resume;
 
    private GameManagerInputSystem gameManagerInput;

    // Start is called before the first frame update

    private void Awake()
    {
        respawn = FindObjectOfType<Respawn>();
        button = FindObjectOfType<Button>();
        pauseMenuL1 = FindObjectOfType<PauseMenuL1>();
        gameManagerInput = new GameManagerInputSystem();
    }
    private void OnEnable()
    {
        gameManagerInput.GameManagerInput.LeftClick.performed += LeftClick;
        gameManagerInput.GameManagerInput.LeftClick.Enable();

        gameManagerInput.GameManagerInput.Escape.performed += Escape;   
        gameManagerInput.GameManagerInput.Escape.Enable();
    }

    private void OnDisable()
    {
        gameManagerInput.GameManagerInput.Escape.performed -= Escape;
        gameManagerInput.GameManagerInput.LeftClick.performed -= LeftClick;
        gameManagerInput.GameManagerInput.LeftClick.Disable();
        gameManagerInput.GameManagerInput.Escape.Disable();
    }
    void Start()
    {
        Time.timeScale = 0f;
        startScreenCamera.SetActive(true);
        gameplayCamera.SetActive(false);
        finishUI.SetActive(false);
        restartMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        finish = false;
        died = false;
        scoreUI.SetActive(false);
        rewindText.SetActive(false);
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (pauseMenuL1.resumeClicked == true)
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            pauseMenuL1.resumeClicked = false;
        }

            if (gameIsPause && player.timeIsStopped && player.rewindAvailable > 0)
            {
                  Time.timeScale = 1f;
                  gameIsPause = false;
                  player.rewindAvailable--;
            }
            if (!gameIsPause && player.timeIsStopped && player.rewindAvailable > 0)
            {
                Time.timeScale = 0f;
                gameIsPause = true;
                player.timeIsStopped = false;
            }
          
        if (gameIsPause && player.rPressedByNew && player.rewindAvailable > 0)
            
        {
            if (player.rPressedByNew)
            {
                Time.timeScale = 1f;
                player.StartRewind();
                
            }
            if (player.rPressedByNew == false)
            {
                player.StopRewind();
                gameIsPause = false;
            }
        }

        if (player.transform.position.x >= 500f || Input.GetKeyDown(KeyCode.F1))
        {
            respawn.posi = new Vector3(0f, 0.5f, 0f);
            finish = true;
            finishUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKey(KeyCode.K))
        {
            Restart();
        }
    }

    public void HitSpike()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        death.Play();
        restartMenu.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(restart, new BaseEventData(eventSystem));
        died = true;
        Time.timeScale = 0f;

    }
    public void EndGame()
    {
        Invoke("Restart", 1f);
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        button.ClickSound();
        died = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(resume, new BaseEventData(eventSystem));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (died == false)
            {
                infoPanel.SetActive(false);
                moveCome = true;
                Time.timeScale = 1f;
                scoreUI.SetActive(true);
                rewindText.SetActive(true);
                startScreenCamera.SetActive(false);
                gameplayCamera.SetActive(true);
                died = true;
            }
        }
    }
    public void Escape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.SetActive(true);
            canvas.enabled = false;
            if (GameObject.Find("HintPanel"))
            {
                hintPanel = GameObject.Find("HintPanel");
                hintPanel.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

/*    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/
}
