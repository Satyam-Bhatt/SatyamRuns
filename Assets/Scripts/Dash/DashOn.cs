using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOn : MonoBehaviour
{
    [SerializeField] GameObject dashText;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject leftClick;

    private DoNotDestroy doNotDestroy;
    private bool leftClickEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        doNotDestroy = FindObjectOfType<DoNotDestroy>();
        dashText.SetActive(false);
        panel.SetActive(false);
        leftClick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1f;
            leftClickEnabled = false;
            dashText.SetActive(false);
            panel.SetActive(false);
            leftClick.SetActive(false);

            gameObject.SetActive(false);

            doNotDestroy.dashDisable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && doNotDestroy.dashDisable == false)
        {
            dashText.SetActive(true);
            panel.SetActive(true);
            leftClick.SetActive(true);
            leftClickEnabled = true;
            Time.timeScale = 0f;
        }
    }
}
