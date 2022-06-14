using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public Transform cameraFinishPosition;
    public Transform templeStartLocation;
    public Transform templeDoorLocation;

    public static float testFloat = 0f;
    private float finalFillValue = 0f;

    void Start()
    {
        StartCoroutine(LateStart(0.5f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cameraFinishPosition = FinishPointScript.instance.cameraFinishPosition;
        templeStartLocation = FinishPointScript.instance.templeStartLocation;
        templeDoorLocation = FinishPointScript.instance.templeDoorLocation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollectableObject")
        {
            other.gameObject.SetActive(false);


            GameManager.collectedItems++;

            if (finalFillValue == 0)
            {
                finalFillValue += Mathf.Pow(2, other.GetComponent<CollectableScript>().type);
                StartCoroutine(ChakraFillBar_Filling());
            }
            else
            {
                finalFillValue += Mathf.Pow(2, other.GetComponent<CollectableScript>().type);
            }

        }
        else if (other.tag == "ObstacleObject")
        {
            Character.instance.PlayObstacleDamageAnimation();
            GameManager.collectedItems--;
            if (GameManager.collectedItems <= 0)
                GameManager.collectedItems = 0;

            if (finalFillValue == 0)
            {
                StartCoroutine(ChakraFillBar_Damage(other.GetComponent<ObstacleScript>().type));
            }
            else
            {
                finalFillValue -= Mathf.Pow(2, other.GetComponent<ObstacleScript>().type);
                if (finalFillValue <= 0)
                {
                    finalFillValue = 0f;
                }
            }
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LevelEnd")
        {
            //Character.instance.ChangeAnimation(isGameEnd: GameManager.isGameEnd);
            StartCoroutine(LerpTo_MiddlePoint(other));
        }
        else if (other.tag == "TempleTrigger")
        {
            this.gameObject.SetActive(false);
            UiManager.instance.gameScreenPanel.SetActive(false);
            UiManager.instance.winScreenPanel.SetActive(true);
            UiManager.instance.fx_WinConfetti.SetActive(true);
        }
    }


    public IEnumerator ChakraFillBar_Damage(int type)
    {
        float timeLapse = 0f;
        float totalTime = 1f;



        float startFill = UiManager.instance.chakraFillBar.fillAmount;
        float finishFill = UiManager.instance.chakraFillBar.fillAmount - type / GameManager.chakraFillValue;

        while (timeLapse <= totalTime)
        {
            UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(startFill, finishFill, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
        }
        if (UiManager.instance.chakraFillBar.fillAmount <= 0f)
        {
            UiManager.instance.chakraFillBar.fillAmount = 0f;
        }
        yield return null;
    }

    public IEnumerator ChakraFillBar_EmptyingFull()
    {
        // GameManager.collectedItems = 0;
        float timeLapse = 0f;
        float totalTime = 1f;

        while (timeLapse <= totalTime)
        {
            UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(1f, 0f, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;

        }

        if (Character.instance.chakraLevel > 5)
            Character.instance.chakraLevel = 5;

        for (int i = 0; i < Character.instance.chakraLevel; i++)
        {
            Character.instance.charakras[i].gameObject.SetActive(true);
        }
        UiManager.instance.chakraImage.GetComponent<Image>().sprite = UiManager.instance.chakraImages[Character.instance.chakraLevel - 1].GetComponent<SpriteRenderer>().sprite;
        UiManager.instance.chakraFillBar.fillAmount = 0f;
        Character.instance.chakraLevel++;
        GameManager.CalculateFillAmount();
        Debug.Log("Chakra Bar is Empty");
    }
    public IEnumerator ChakraFillBar_Filling()
    {

        while (finalFillValue > 0)
        {
            float timeLapse = 0f;
            float totalTime = 1f;

            float startFill = UiManager.instance.chakraFillBar.fillAmount;
            float finishFill = UiManager.instance.chakraFillBar.fillAmount + ((Mathf.Pow(2, Mathf.Log(finalFillValue, 2))) / GameManager.chakraFillValue);
            while (timeLapse <= totalTime)
            {
                timeLapse += Time.deltaTime;

                if (finishFill >= 1f)
                {

                    UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(startFill, 1f, timeLapse / totalTime);
                    if (timeLapse / totalTime >= 1f)
                    {
                        finalFillValue = finalFillValue - GameManager.chakraFillValue;
                        StartCoroutine(ChakraImageScaling());
                        yield return new WaitForSeconds(1.55f);
                    }
                }
                else
                {
                    UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(startFill, finishFill, timeLapse / totalTime);
                    if (timeLapse / totalTime >= 1)
                    {
                        UiManager.instance.chakraFillBar.fillAmount = finishFill;
                        if (finalFillValue < GameManager.chakraFillValue)
                        {
                            finalFillValue = 0;
                        }
                    }
                }


                yield return null;
            }
        }
    }

    public IEnumerator ChakraImageScaling()
    {
        float timeLapse = 0f;
        float totalTime = 0.5f;

        while (timeLapse <= totalTime)
        {
            UiManager.instance.chakraImages[Character.instance.chakraLevel - 1].transform.localScale = Vector3.one * animationCurve.Evaluate(timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ChakraFillBar_EmptyingFull());

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

            timeLapse += Time.deltaTime;
            yield return null;
        }
        GameManager.isGameEnd = true;
        Character.instance.ChangeAnimation(isGameEnd: GameManager.isGameEnd);
        Character.instance.cloud.SetActive(true);
        StartCoroutine(LerpCameraTo_FinishPosition());
    }


    public IEnumerator LerpCameraTo_FinishPosition()
    {
        float timeLapse = 0f;
        float totalTime = 1f;
        Vector3 startPosition = Camera.main.transform.position;
        Quaternion startRotation = Camera.main.transform.rotation;
        while (timeLapse <= totalTime)
        {
            Camera.main.transform.position = Vector3.Lerp(startPosition, cameraFinishPosition.position, timeLapse / totalTime);
            Camera.main.transform.rotation = Quaternion.Lerp(startRotation, cameraFinishPosition.rotation, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        CameraMovement.levelEndOffset = Camera.main.transform.position - Camera.main.GetComponent<CameraMovement>().Player.transform.position;
    }

    public IEnumerator LerpCharacterTo_LevelEndMultipliers()
    {
        float finishPointMultiplier = GameManager.collectedItems / GameManager.levelCollectableCount + testFloat;
        if (finishPointMultiplier >= 0.9f)
            finishPointMultiplier = 1f;

        GameManager.lerpCharacterTo_LevelEndMultipliers_isCalled = true;
        float timeLapse = 0f;
        float totalTime = 2f;

        Vector3 characterStartLocation = Character.instance.transform.position;

        Vector3 characterEndLocation = new Vector3(characterStartLocation.x,
            templeStartLocation.position.y * finishPointMultiplier,
            characterStartLocation.z);

        while (timeLapse <= totalTime)
        {
            Character.instance.transform.position = Vector3.Lerp(characterStartLocation,
                characterEndLocation,
                timeLapse / totalTime);

            timeLapse += Time.deltaTime;
            yield return null;
        }

        if (finishPointMultiplier < 0.9f)
        {
            GameManager.isLevelEnd = true;
            UiManager.instance.gameScreenPanel.SetActive(false);
            UiManager.instance.winScreenPanel.SetActive(true);
            UiManager.instance.fx_WinConfetti.SetActive(true);

        }
        else
        {
            StartCoroutine(LerpCharacterTo_Temple());
        }
    }

    public IEnumerator LerpCharacterTo_Temple()
    {
        float timeLapse = 0f;
        float totalTime = 4f;

        Vector3 characterStartLocation = Character.instance.transform.position;

        while (timeLapse <= totalTime)
        {
            Character.instance.transform.position = Vector3.Lerp(characterStartLocation,
                templeDoorLocation.position,
                timeLapse / totalTime);

            timeLapse += Time.deltaTime;
            yield return null;
        }
    }

}
