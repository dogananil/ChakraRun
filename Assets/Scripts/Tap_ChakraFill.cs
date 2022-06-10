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
                StartCoroutine(TapTextScaling());

            GameManager.countdownTimeLapse -= Time.deltaTime;
            if (GameManager.countdownTimeLapse > 0f)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    UiManager.instance.chakraFillBar.fillAmount += GameManager.tapPower;

                    if (UiManager.instance.chakraFillBar.fillAmount >= 1)
                    {
                        UiManager.instance.chakraFillBar.fillAmount = 1;
                        GameManager.countdownTimeLapse = 0f;
                    }
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
