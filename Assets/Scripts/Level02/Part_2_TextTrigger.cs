using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Part_2_TextTrigger : MonoBehaviour
{
    [SerializeField] private TMP_Text hintTrigger;
    [SerializeField] private TMP_Text hint;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject leftClick;
    [SerializeField] private GameObject panel2;
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
        panel2.SetActive(false);

        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if (hintEnabled)
        {
            if (Input.GetKey(KeyCode.H))
            {
                panel.SetActive(true);
                hint.enabled = true;
                leftClick.SetActive(true);
                Time.timeScale = 0f;
                leftClickEnabled = true;

                hintTrigger.enabled = false;
                panel2.SetActive(false);
                score.SetActive(false);
            }
            if(Input.GetKey(KeyCode.Mouse0) && leftClickEnabled)
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
        if(other.gameObject.layer == 6)
        {
            hintTrigger.enabled = true;
            panel2.SetActive(true);
            hintEnabled = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            hintTrigger.enabled = false;
            panel2.SetActive(false);
            hintEnabled = false;
            animator.SetBool("Disappear", true);
        }
    }
}
