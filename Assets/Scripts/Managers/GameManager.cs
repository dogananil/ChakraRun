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

    private void Start()
    {
        PoolManager.instance.CreatePool();
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
    }
}
