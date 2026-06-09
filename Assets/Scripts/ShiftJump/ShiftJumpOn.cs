using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftJumpOn : MonoBehaviour
{
    [SerializeField] GameObject shiftJump;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject leftClick;

    private bool leftClickEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        shiftJump.SetActive(false);
        panel.SetActive(false);
        leftClick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(leftClickEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            shiftJump.SetActive(false);
            panel.SetActive(false);
            leftClick.SetActive(false);
            leftClickEnabled = false;
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            shiftJump.SetActive(true);
            panel.SetActive(true);
            leftClick.SetActive(true);
            leftClickEnabled = true;
            Time.timeScale = 0f;
        }
    }
}
