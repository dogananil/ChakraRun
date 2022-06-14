using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_ChakraFill : MonoBehaviour
{
    public AnimationCurve animationCurve;
    private void FixedUpdate()
    {

        if (GameManager.isGameStart == true && GameManager.isGameEnd == true)
        {
            if (GameManager.tapTextScaling_isCalled == false)
            {
                StartCoroutine(TapTextScaling());
                Character.instance.levelEndFx.SetActive(true);
            }

            GameManager.countdownTimeLapse -= Time.deltaTime;
            if (GameManager.countdownTimeLapse > 0f)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    UiManager.instance.chakraFillBar.fillAmount += GameManager.tapPower;
                    Character.instance.levelEndFx.transform.localScale += Vector3.one * 0.2f;

                    if (UiManager.instance.chakraFillBar.fillAmount >= 1)
                    {
                        UiManager.instance.chakraFillBar.fillAmount = 1;
                        GameManager.countdownTimeLapse = 0f;
                        Character.instance.levelEndFx.SetActive(false);
                    }
                }
            }
            else
            {
                if (GameManager.lerpCharacterTo_LevelEndMultipliers_isCalled == false)
                {
                    TriggerScript.testFloat = UiManager.instance.chakraFillBar.fillAmount;
                    Character.instance.levelEndFx.SetActive(false);
                    StartCoroutine(GetComponent<TriggerScript>().LerpCharacterTo_LevelEndMultipliers());

                }
            }


        }
    }

    public IEnumerator TapTextScaling()
    {
        UiManager.instance.tapText.gameObject.SetActive(true);
        float timeLapse = 0.0f;
        float totalTime = 5f;
        GameManager.tapTextScaling_isCalled = true;

        while (timeLapse <= totalTime && UiManager.instance.chakraFillBar.fillAmount < 1)
        {
            UiManager.instance.tapText.gameObject.transform.localScale = Vector3.one * animationCurve.Evaluate(timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        UiManager.instance.tapText.gameObject.transform.localScale = Vector3.one;
        UiManager.instance.tapText.gameObject.SetActive(false);

        //GameManager.totalGold
    }
}
