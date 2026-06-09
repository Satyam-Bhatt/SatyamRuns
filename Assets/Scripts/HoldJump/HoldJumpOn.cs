using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldJumpOn : MonoBehaviour
{

    [SerializeField] private GameObject holdJumpText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject leftClick;

    private bool leftClickEnabled = false;
    private DoNotDestroy doNotDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        doNotDestroy = FindObjectOfType<DoNotDestroy>();
        holdJumpText.SetActive(false);
        panel.SetActive(false);
        leftClick.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            holdJumpText.SetActive(false);
            panel.SetActive(false);
            leftClick.SetActive(false);
            gameObject.SetActive(false);
            leftClickEnabled = false;
            doNotDestroy.holdSpaceDisable = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && doNotDestroy.holdSpaceDisable == false)
        {
            Time.timeScale = 0f;
            holdJumpText.SetActive(true);
            leftClick.SetActive(true);
            panel.SetActive(true);
            leftClickEnabled = true;
        }
    }
}
