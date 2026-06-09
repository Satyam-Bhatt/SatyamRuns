using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Part_6_T1 : MonoBehaviour
{
    [SerializeField] TMP_Text hint;
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text actualHint;
    [SerializeField] GameObject leftClick;
    [SerializeField] GameObject panelForHint;
     GameObject score;

    private bool hintEnabled = false;
    private bool leftClickEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        hint.enabled = false;
        panel.SetActive(false);
        actualHint.enabled = false;
        leftClick.SetActive(false);
        panelForHint.SetActive(false);

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if(hintEnabled && Input.GetKeyDown(KeyCode.H))
        {
            panel.SetActive(true);
            actualHint.enabled = true;
            Time.timeScale = 0f;
            leftClickEnabled = true;
            leftClick.SetActive(true);
            score.SetActive(false);
            panelForHint.SetActive(false);
            hint.enabled = false;
        }

        if(leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
            leftClick.SetActive(false);
            actualHint.enabled = false;
            score.SetActive(true);
            hint.enabled = true;
            panelForHint.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            hintEnabled = true;
            hint.enabled = true;
            panelForHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            hint.enabled=false;
            hintEnabled = false;
            panelForHint.SetActive(false);
        }
    }
}
