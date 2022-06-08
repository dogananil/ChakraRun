using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMovement : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        if (GameManager.isGameStart == true && GameManager.isGameEnd == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
