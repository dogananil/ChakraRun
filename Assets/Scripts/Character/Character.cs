using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterLevel = 0;
    public int chakraLevel = 1;
    public GameObject cloud;
    public Animator animator;
    public static Character instance; 
    public List<GameObject> charakras = new List<GameObject>();

    public GameObject levelEndFx;
    public Vector3 levelEndFx_DefaultLocalScale;

    public GameObject mainParent;
    public GameObject characterParent;

    public GameObject levelUpFx;


    public Vector3 characterStartLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        characterStartLocation = transform.localPosition;
        levelEndFx_DefaultLocalScale = levelEndFx.transform.localScale;
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
