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
    [SerializeField]private Vector3 cameraFollowPosition;
    [SerializeField]private Vector3 m_PlayerStartPosition;
    private bool m_CameraStartMovemmentBool;
    [SerializeField] private Transform cameraGamePlayTransform;
 
   
    [SerializeField] private float m_smoothSpeed;

    void Start()
    {
        
        cameraStartLocation = transform.localPosition;
        cameraStartRotation = transform.rotation;

        m_PlayerStartPosition = Player.transform.position;
        startingOffset = cameraStartLocation - Player.transform.position;
        levelEndOffset = startingOffset;
    }
    void Update()
    {
        if (GameManager.isGameEnd == false && m_CameraStartMovemmentBool)
        {
            if(Player.transform.parent.transform.position.x>4)
            {
               
                
                cameraFollowPosition = new Vector3(1.1f * cameraGamePlayTransform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
               
                

            }
            else if(Player.transform.parent.transform.position.x < -4)
            {
                
                cameraFollowPosition = new Vector3(0.9f* cameraGamePlayTransform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            }
            else
            {
               
                cameraFollowPosition = new Vector3(cameraGamePlayTransform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
               
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                  cameraFollowPosition, m_smoothSpeed);

        }
        else if (GameManager.isLevelEnd == false)
        {

           /* transform.position = Vector3.Lerp(transform.position,
   new Vector3(transform.position.x, Player.transform.position.y + levelEndOffset.y, transform.position.z), Time.deltaTime * 5f);*/
        }
        
        
        
    }
    public IEnumerator CameraGameStartMovement()
    {
        float timeLapse = 0, totalTime = 1.5f;

        while(timeLapse<=totalTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,cameraGamePlayTransform.localPosition, timeLapse / totalTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, cameraGamePlayTransform.localRotation, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = cameraGamePlayTransform.localPosition;
        transform.localRotation = cameraGamePlayTransform.localRotation;
        m_CameraStartMovemmentBool = true;
    }
}
