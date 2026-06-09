using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftSprintOn : MonoBehaviour
{
    private DoNotDestroy doNotDestroy;
    [SerializeField] GameObject shiftOnText;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject leftClick;

    private bool leftClickEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        doNotDestroy = FindObjectOfType<DoNotDestroy>();
        shiftOnText.SetActive(false);
        panel.SetActive(false);
        leftClick.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            shiftOnText.SetActive(false);
            panel.SetActive(false);
            leftClick.SetActive(false);
            leftClickEnabled = false;
            gameObject.SetActive(false);
            doNotDestroy.sprintDisable = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && doNotDestroy.sprintDisable == false)
        {
            shiftOnText.SetActive(true);
            leftClick.SetActive(true);
            panel.SetActive(true);
            leftClickEnabled = true;
            Time.timeScale = 0f; 
        }
    }
}
