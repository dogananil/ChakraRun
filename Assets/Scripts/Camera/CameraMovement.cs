using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;

    Vector3 offset;
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - Player.transform.position;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Player.transform.position.x, Player.transform.position.y + offset.y, Player.transform.position.z + offset.z), Time.deltaTime * 5f);
    }
}
