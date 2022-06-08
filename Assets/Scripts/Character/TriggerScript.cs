using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollectableObject")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ObstacleObject")
        {
            Character.instance.PlayObstacleDamageAnimation();
            //character.ChangeAnimation(isHited: true);
            //character.ChangeAnimation(isHited: false);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LevelEnd")
        {
            GameManager.isGameEnd = true;
            Character.instance.ChangeAnimation(isGameEnd: GameManager.isGameEnd);
           StartCoroutine(LerpTo_MiddlePoint(other));
        }
    }

    public IEnumerator LerpTo_MiddlePoint(Collider other)
    {
        float timeLapse = 0f;
        float totalTime = 1f;
        Vector3 characterStartLocation = Character.instance.transform.position;
        while (timeLapse <= totalTime)
        {
            Character.instance.transform.position = Vector3.Lerp(characterStartLocation,
                other.gameObject.transform.GetComponent<Renderer>().bounds.center,
                timeLapse / totalTime);

            timeLapse += Time.deltaTime;
            yield return null;
        }
    }
}
