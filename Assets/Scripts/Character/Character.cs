using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public static Character instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeAnimation(bool isGameStart = false, bool isGameEnd = false)
    {
        animator.SetBool(nameof(isGameStart), isGameStart);
        animator.SetBool(nameof(isGameEnd), isGameEnd);
    }

    public void PlayObstacleDamageAnimation()
    {
        animator.Play("ObstacleDamage");
    }

   
}
