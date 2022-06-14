using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public int type;
    public void Update()
    {
        transform.Rotate(new Vector3(0f, 35f, 0f) * Time.deltaTime);
    }
}
