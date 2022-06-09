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
        Vector3 characterEndLocation = other.gameObject.transform.GetComponent<Renderer>().bounds.center;
        while (timeLapse <= totalTime)
        {
            Character.instance.transform.position = Vector3.Lerp(characterStartLocation,
                characterEndLocation,
                timeLapse / totalTime);

            if (Vector3.Distance(Character.instance.transform.position, characterEndLocation) < 1f)
                break;

            timeLapse += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
