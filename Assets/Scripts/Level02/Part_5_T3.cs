using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Part_5_T3 : MonoBehaviour
{
    [SerializeField] private TMP_Text hintTrigger;
    [SerializeField] private TMP_Text hint;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject leftClick;
    [SerializeField] private GameObject panelHint;
    private GameObject score;
    [SerializeField] private Animator animator;

    private bool hintEnabled = false;
    private bool leftClickEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        hintTrigger.enabled = false;
        hint.enabled = false;
        panel.SetActive(false);
        leftClick.SetActive(false);
        panelHint.SetActive(false);

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if (hintEnabled)
        {
            if (Input.GetKey(KeyCode.H))
            {
                Time.timeScale = 0f;
                panel.SetActive(true);
                hint.enabled = true;
                leftClick.SetActive(true);
                leftClickEnabled = true;
                score.SetActive(false);

               /* hintTrigger.enabled = false;
                panelHint.SetActive(false);*/
            }

            if (Input.GetKey(KeyCode.Mouse0) && leftClickEnabled)
            {
                Time.timeScale = 1f;
                panel.SetActive(false);
                hint.enabled = false;
                leftClick.SetActive(false);
                score.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            hintTrigger.enabled = true;
            hintEnabled = true;
            panelHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            hintTrigger.enabled = false;
            hintEnabled = false;
            panelHint.SetActive(false);
        }
    }
}
