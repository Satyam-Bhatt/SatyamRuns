using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WASD : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text move;

    private Game game;
    private JumpOpen jump;

    private float time;

    private bool runOnce = true;
    // Start is called before the first frame update
    void Awake()
    {
        game = FindObjectOfType<Game>();
        jump = FindObjectOfType<JumpOpen>();
        move.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.moveCome && runOnce)
        {
            move.enabled = true;
            time = Time.time;
            runOnce = false;

        }
     
        if(jump.closeMove || (Time.time - time) > 5f)
        {
            animator.SetBool("Close", true);
        }
    }
}
