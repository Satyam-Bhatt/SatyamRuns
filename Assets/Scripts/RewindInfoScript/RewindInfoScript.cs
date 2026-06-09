using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewindInfoScript : MonoBehaviour
{
   // [SerializeField] TMP_Text Rtorewind;
   // [SerializeField] TMP_Text Ftofreeze;
    [SerializeField] GameObject panel;
    // [SerializeField] GameObject leftClick;

    private bool leftClickEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && leftClickEnable)
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            leftClickEnable = true;
            panel.SetActive(true);
            Time.timeScale = 0f;
           
        }
    }
}
