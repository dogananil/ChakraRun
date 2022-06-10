using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameEnd = false;
    public static int winGold = 0;
    public static int minimumGold = 0;
    public static int levelNumber = 0;
    public static int totalGold = 0;
    public static int totalLevelCount = 0;

    public static int collectedItems = 0;
    public static float chakraFillValue = 0;

    public static float countdownTimeLapse = 5f;
    public static bool tapTextScaling_isCalled = false;


    public static float tapPower = 0.1f;


    private void Start()
    {
        PoolManager.instance.CreatePool();
        CalculateFillAmount();
        LoadPrefs();
        LevelManager.instance.LoadLevels();
        totalLevelCount = LevelManager.levelDatas.Count;
        LevelManager.instance.CreateLevel();

    }
    private void LoadPrefs()
    {
        totalGold = PlayerPrefs.GetInt("Gold", 0);
        levelNumber = PlayerPrefs.GetInt("LevelNumber", 0);
    }
    public static void SavePrefs()
    {
        PlayerPrefs.SetInt("Gold", totalGold);
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.Save();
    }

    public static void ResetDefaults()
    {
        isGameStart = false;
        isGameEnd = false;
        chakraFillValue = 0;
        countdownTimeLapse = 5f;
        collectedItems = 0;
        tapTextScaling_isCalled = false;
        foreach (var chakra in Character.instance.charakras)
        {
            chakra.gameObject.SetActive(false);
        }
    }

    public static void CalculateFillAmount()
    {
        int tempSum = 0;
        for (int i = 0; i < Character.instance.chakraLevel; i++)
        {
            tempSum += (int)Mathf.Pow(3, i);
        }

        chakraFillValue = Mathf.Pow(3, Character.instance.chakraLevel) - tempSum + 1;
    }
}
