using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointScript : MonoBehaviour
{
    public static FinishPointScript instance;
    public Transform templeStartLocation;
    public Transform cameraFinishPosition;
    public Transform templeDoorLocation;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
