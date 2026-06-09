using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Part_7_T10 : MonoBehaviour
{
    [SerializeField] private TMP_Text hint;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text actualHint;
    [SerializeField] private GameObject leftClick;
    [SerializeField] private GameObject fallDetector;
    [SerializeField] private GameObject fallBlocks;
    [SerializeField] private GameObject panelForHint;
    private GameObject score;
 
    private bool hintEnabled = false;
    private bool leftClickEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        hint.enabled = false;
        panel.SetActive(false);
        actualHint.enabled = false;
        leftClick.SetActive(false);
        fallDetector.SetActive(false);
        fallBlocks.SetActive(false);
        panelForHint.SetActive(false);

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if (hintEnabled && Input.GetKeyDown(KeyCode.H))
        {
            panel.SetActive(true);
            actualHint.enabled = true;
            Time.timeScale = 0f;
            leftClickEnabled = true;
            leftClick.SetActive(true);
            score.SetActive(false);
        }

        if (leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
            leftClick.SetActive(false);
            actualHint.enabled = false;
            score.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            hintEnabled = true;
            hint.enabled = true;
            fallDetector.SetActive(true);
            panelForHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            hint.enabled = false;
            hintEnabled = false;
            panelForHint.SetActive(false);
        }
    }
}
