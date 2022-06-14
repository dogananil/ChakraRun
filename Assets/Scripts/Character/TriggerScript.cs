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

    private bool successText_isCalled = false;

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
            other.GetComponent<CollectableScript>().particleFx.transform.SetParent(null);
            other.GetComponent<CollectableScript>().particleFx.SetActive(true);
            other.gameObject.SetActive(false);
            StartCoroutine(LerpPlus1Text());
            if (successText_isCalled == false)
                StartCoroutine(LerpSuccessTexts());

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
            other.GetComponent<ObstacleScript>().particleFx.transform.SetParent(null);
            other.GetComponent<ObstacleScript>().particleFx.SetActive(true);
            other.gameObject.SetActive(false);
            StartCoroutine(LerpMinus1Text());

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
        Character.instance.levelUpFx.gameObject.SetActive(true);
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

    public IEnumerator LerpSuccessTexts()
    {
        successText_isCalled = true;
        float timeLapse = 0f;
        float totalTime = 1f;


        var randomNumber = Random.Range(0, 3);
        var randomSuccessText = UiManager.instance.texts[randomNumber];

        Vector3 textStartingLocation = randomSuccessText.transform.localPosition;
        Vector3 textFinishLocation = textStartingLocation + new Vector3(0f, 750f, 0f);

        randomSuccessText.gameObject.SetActive(true);
        while (timeLapse < totalTime)
        {
            randomSuccessText.transform.localPosition = Vector3.Lerp(textStartingLocation, textFinishLocation, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        randomSuccessText.gameObject.SetActive(false);
        randomSuccessText.transform.localPosition = textStartingLocation;

        successText_isCalled = false;
    }

    public IEnumerator LerpPlus1Text()
    {
        float timeLapse = 0f;
        float totalTime = 0.8f;
        var txt_Plus1 = UiManager.instance.txt_Plus1;
        txt_Plus1.gameObject.SetActive(true);

        Vector3 StartingLocation = txt_Plus1.transform.localPosition;
        Vector3 FinishLocation = StartingLocation + new Vector3(0f, -15f, 0f);

        while (timeLapse < totalTime)
        {
            txt_Plus1.transform.localPosition = Vector3.Lerp(StartingLocation, FinishLocation, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        txt_Plus1.gameObject.SetActive(false);
        txt_Plus1.transform.localPosition = StartingLocation;
    }

    public IEnumerator LerpMinus1Text()
    {
        float timeLapse = 0f;
        float totalTime = 0.8f;
        var txt_Minus1 = UiManager.instance.txt_Minus1;
        txt_Minus1.gameObject.SetActive(true);

        Vector3 StartingLocation = txt_Minus1.transform.localPosition;
        Vector3 FinishLocation = StartingLocation + new Vector3(0f, -15f, 0f);

        while (timeLapse < totalTime)
        {
            txt_Minus1.transform.localPosition = Vector3.Lerp(StartingLocation, FinishLocation, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        txt_Minus1.gameObject.SetActive(false);
        txt_Minus1.transform.localPosition = StartingLocation;
    }

}
