using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameEnd = false;
    public static bool isLevelEnd = false;

    public static int winGold = 0;
    public static int minimumGold = 0;
    public static int levelNumber = 0;
    public static int totalGold = 0;
    public static int totalLevelCount = 0;

    public static int collectedItems = 0;
    public static int levelCollectableCount = 0;
    public static float chakraFillValue = 0;

    public static float countdownTimeLapse = 5f;
    public static bool lerpCharacterTo_LevelEndMultipliers_isCalled = false;
    public static bool tapTextScaling_isCalled = false;


    public static float tapPower = 0.05f;

    private void Start()
    {
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("LevelNumber", 0);
        PlayerPrefs.Save();




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
        levelCollectableCount = 0;
        collectedItems = 0;
        tapTextScaling_isCalled = false;
        lerpCharacterTo_LevelEndMultipliers_isCalled = false;
        UiManager.instance.chakraFillBar.transform.parent.gameObject.SetActive(true);
        foreach (var chakra in Character.instance.charakras)
        {
            chakra.gameObject.SetActive(false);
        }

        Camera.main.transform.position = CameraMovement.cameraStartLocation;
        Camera.main.transform.rotation = CameraMovement.cameraStartRotation;

        Character.instance.transform.localPosition = Character.instance.characterStartLocation;
        Character.instance.cloud.SetActive(false);
        Character.instance.mainParent.transform.position = new Vector3(0f,0f,1f);
        Character.instance.characterParent.transform.localPosition= Vector3.zero;
        Character.instance.transform.localPosition = Vector3.zero;
        Character.instance.gameObject.SetActive(false);
        Character.instance.gameObject.SetActive(true);
        Character.instance.levelEndFx.transform.localScale = Character.instance.levelEndFx_DefaultLocalScale;
        Character.instance.levelEndFx.SetActive(false);

        Character.instance.chakraLevel = 1;
        Character.instance.characterLevel = 0;
        for (int i = 0; i < Character.instance.charakras.Count; i++)
        {
            Character.instance.charakras[i].gameObject.SetActive(false);
        }

        UiManager.instance.chakraFillBar.fillAmount = 0f;
        UiManager.instance.chakraImage.GetComponent<Image>().sprite = UiManager.instance.chakraImages[0].GetComponent<SpriteRenderer>().sprite;



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
