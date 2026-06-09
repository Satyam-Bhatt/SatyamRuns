using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOpen : MonoBehaviour
{
    private DoNotDestroy doNotDestroy;

     [SerializeField] private GameObject jumpInfo;
    [SerializeField] private GameObject leftClick;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator animator;

    private bool leftClickEnabled = false;

    [HideInInspector]
    public bool closeMove = false;

    // Start is called before the first frame update
    void Awake()
    {
        doNotDestroy = FindObjectOfType<DoNotDestroy>();

        jumpInfo.SetActive(false);
        leftClick.SetActive(false);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            jumpInfo.SetActive(false);
            panel.SetActive(false);
            leftClick.SetActive(false);

            leftClickEnabled = false;
            doNotDestroy.spaceDisable = true;
            
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && doNotDestroy.spaceDisable == false)
        {
            panel.SetActive(true);
            jumpInfo.SetActive(true);
            leftClick.SetActive(true);
            leftClickEnabled = true;
            closeMove = true;
            Time.timeScale = 0f;

            
        }
    }
}
