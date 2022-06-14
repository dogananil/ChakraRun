using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;

    public static Vector3 startingOffset;
    public static Vector3 levelEndOffset;
    public static Vector3 cameraStartLocation;
    public static Quaternion cameraStartRotation;

    void Start()
    {
        cameraStartLocation = transform.position;
        cameraStartRotation = transform.rotation;
        startingOffset = cameraStartLocation - Player.transform.position;
        levelEndOffset = startingOffset;
    }
    void Update()
    {
        if (GameManager.isGameEnd == false)
        {
            transform.position = Vector3.Lerp(transform.position,
               new Vector3(Player.transform.position.x, Player.transform.position.y + startingOffset.y, Player.transform.position.z + startingOffset.z), Time.deltaTime * 5f);
        }
        else if (GameManager.isLevelEnd == false)
        {

            transform.position = Vector3.Lerp(transform.position,
   new Vector3(transform.position.x, Player.transform.position.y + levelEndOffset.y, transform.position.z), Time.deltaTime * 5f);
        }
    }
}
