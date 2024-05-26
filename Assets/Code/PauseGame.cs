using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gameOver = false;
    public Animator animator;

    void Update()
    {
        if (gameOver == true)
        {
            animator.SetBool("GameOver", true);
            End();
        }
    }

    void End()
    {
        Time.timeScale = .1f;
    }

    void Pause()
    {
        Time.timeScale = 1;
    }

    void Resume()
    {
        Time.timeScale = 1;
    }
}
