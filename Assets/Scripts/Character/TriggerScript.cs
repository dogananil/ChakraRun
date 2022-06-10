using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public AnimationCurve animationCurve;
    private float finalFillValue = 0f;

    private Coroutine coroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollectableObject")
        {
            other.gameObject.SetActive(false);
            GameManager.collectedItems++;

            StartCoroutine(EmptyCoroutine(other));
        }
        else if (other.tag == "ObstacleObject")
        {
            Character.instance.PlayObstacleDamageAnimation();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LevelEnd")
        {
            //Character.instance.ChangeAnimation(isGameEnd: GameManager.isGameEnd);
            StartCoroutine(LerpTo_MiddlePoint(other));
        }
    }
    private IEnumerator EmptyCoroutine(Collider other)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(ChakraFillBar_Filling(/*other.GetComponent<CollectableScript>().type*/3));
        }
        else
        {
            yield return coroutine;
            coroutine = null;
        }
    }

    public IEnumerator ChakraFillBar_Damage(int type)
    {
        float timeLapse = 0f;
        float totalTime = 1f;

        float startFill = (GameManager.collectedItems - 1) / GameManager.chakraFillValue;
        float finishFill = (GameManager.collectedItems) / GameManager.chakraFillValue;
        yield return null;
    }

    public IEnumerator ChakraFillBar_EmptyingFull()
    {
        GameManager.collectedItems = 0;
        float timeLapse = 0f;
        float totalTime = 1f;

        while (timeLapse <= totalTime)
        {
            UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(1f, 0f, timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;

        }
        for (int i = 0; i < Character.instance.chakraLevel; i++)
        {
            Character.instance.charakras[i].gameObject.SetActive(true);
            /*var testMaterial = Character.instance.charakras[i].gameObject.GetComponent<Renderer>().material;
            testMaterial.EnableKeyword("_EMISSION");
            testMaterial.SetColor("_EmissionColor", testMaterial.color);*/
        }
        UiManager.instance.chakraFillBar.fillAmount = 0f;
        Character.instance.chakraLevel++;
        GameManager.CalculateFillAmount();
    }
    public IEnumerator ChakraFillBar_Filling(float type /*0-1-2-3*/)
    {
        float timeLapse = 0f;
        float totalTime = 1f;

        float startFill = UiManager.instance.chakraFillBar.fillAmount;
        float finishFill = UiManager.instance.chakraFillBar.fillAmount + Mathf.Pow(2, type) / GameManager.chakraFillValue;// 16/3

        while (timeLapse <= totalTime)
        {
            timeLapse += Time.deltaTime;

            if (finishFill > 1f)
            {
                finalFillValue += Mathf.Pow(2, type);

                UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(startFill, 1f, timeLapse / totalTime);
                if (timeLapse / totalTime >= 1f)
                {
                    var temp2 = Mathf.Pow(2, type) - GameManager.chakraFillValue;

                    StartCoroutine(ChakraImageScaling());
                    yield return new WaitForSeconds(1.55f);
                    StartCoroutine(ChakraFillBar_Filling(Mathf.Log(temp2, 2)));
                }
            }
            else
            {
                UiManager.instance.chakraFillBar.fillAmount = Mathf.Lerp(startFill, finishFill, timeLapse / totalTime);
            }

            coroutine = null;
            yield return null;
        }

        /*if (finishFill >= 1f)
            UiManager.instance.chakraFillBar.fillAmount = 1f;

        if (UiManager.instance.chakraFillBar.fillAmount >= 1)
        {
            StartCoroutine(ChakraImageScaling());
        }*/
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

            if (Vector3.Distance(Character.instance.transform.position, characterEndLocation) < 1f)
                break;

            timeLapse += Time.deltaTime;
            yield return null;
        }
        GameManager.isGameEnd = true;
        Character.instance.ChangeAnimation(isGameEnd: GameManager.isGameEnd);
    }
}
