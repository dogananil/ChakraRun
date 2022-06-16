using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Coroutine coroutine;
    
    public GameObject goldImage;
    public Transform startGoldPosition, finishGoldPosition;
    
    public IEnumerator goldAnimation(int winGold=55)
    {
        for(int i=0;i<winGold;i++)
        {
            float timeLapse = 0f;
            float totalTime = 0.15f;

            var tempGold = Instantiate(goldImage, startGoldPosition);
            tempGold.transform.position = startGoldPosition.position;

            while (timeLapse <= totalTime)
            {
                tempGold.transform.position = Vector3.Lerp(startGoldPosition.position, finishGoldPosition.position, timeLapse / totalTime);
                timeLapse += Time.deltaTime;
                yield return null;
            }
            Destroy(tempGold.gameObject);
        }
    }
    
    public void btn_NextLevelClick()
    {
        StartCoroutine(goldAnimation());
        UiManager.instance.fx_WinConfetti.SetActive(false);
        PoolManager.instance.ResetPool();
        UiManager.instance.chakraFillBar.fillAmount = 0;

        GameManager.levelNumber++;
        PlayerPrefs.SetInt("LevelNumber", GameManager.levelNumber);

        if (GameManager.levelNumber > GameManager.totalLevelCount)
        {
            GameManager.levelNumber = UnityEngine.Random.Range(5, GameManager.totalLevelCount);
        }
        GameManager.ResetDefaults();
        GameManager.totalGold += GameManager.winGold;
        GameManager.SavePrefs();


        UiManager.instance.winScreenPanel.SetActive(false);
        UiManager.instance.fx_WinConfetti.SetActive(false);

        LevelManager.instance.CreateLevel();

       UiManager.instance.StartPanel.SetActive(true);
    }
    public void btn_RestartLevelClick()
    {
        PoolManager.instance.ResetPool();
        UiManager.instance.chakraFillBar.fillAmount = 0;

        GameManager.ResetDefaults();
        GameManager.totalGold += GameManager.minimumGold;
        GameManager.SavePrefs();

        UiManager.instance.loseScreenPanel.SetActive(false);

        LevelManager.instance.CreateLevel();

        UiManager.instance.StartPanel.SetActive(true);

    }



}
