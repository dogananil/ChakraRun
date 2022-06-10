using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static List<LevelData> levelDatas = new List<LevelData>();

    public GameObject platformParent;
    public GameObject obstacleParent;
    public GameObject collectableParent;


    public List<PlatformScript> currentLevelPlatforms;
    public List<CollectableScript> currentLevelCollectables;
    public List<ObstacleScript> currentLevelObstacles;

    public GameObject mainParent;

    private Vector3 mainParentDefaultTransform = new Vector3(0f, 0f, 1f);
    private Vector3 platformDefaultTransform = new Vector3(0f, 0f, 60f);



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadLevels()
    {
        for (int i = 0; i < Resources.LoadAll<TextAsset>("Levels").Length; i++)
        {
            TextAsset jsonInfo = Resources.Load<TextAsset>("Levels/Level" + i);
            levelDatas.Add(JsonUtility.FromJson<LevelData>(jsonInfo.text));
        }
    }
    public void CreateLevel()
    {
        SetLevelVariables();
        SetTextValues();
        mainParent.transform.position = mainParentDefaultTransform;
        int platformIndex = 0;

        foreach (var platform in levelDatas[GameManager.levelNumber].platforms)
        {
            currentLevelPlatforms.Add(PoolManager.instance.platforms[PoolManager.instance.platforms.Count - 1]);
            PlatformScript tempPlatform = currentLevelPlatforms[currentLevelPlatforms.Count - 1];
            PoolManager.instance.platforms.RemoveAt(PoolManager.instance.platforms.Count - 1);

            tempPlatform.transform.SetParent(platformParent.transform);
            tempPlatform.transform.localPosition = platformDefaultTransform * platformIndex;
            tempPlatform.transform.gameObject.SetActive(true);

            foreach (var collectable in platform.collectables)
            {
                var collectablePosition = collectable.position.Split('#');

                currentLevelCollectables.Add(PoolManager.instance.collectables[PoolManager.instance.collectables.Count - 1]);
                CollectableScript tempCollectable = currentLevelCollectables[currentLevelCollectables.Count - 1];
                PoolManager.instance.collectables.RemoveAt(PoolManager.instance.collectables.Count - 1);

                tempCollectable.transform.SetParent(collectableParent.transform);
                tempCollectable.transform.localPosition = new Vector3(float.Parse(collectablePosition[0]), 2f, float.Parse(collectablePosition[1])) + platformDefaultTransform * platformIndex;
                tempCollectable.transform.gameObject.SetActive(true);
            }
            foreach (var obstacle in platform.obstacles)
            {
                var obstaclePosition = obstacle.position.Split('#');

                currentLevelObstacles.Add(PoolManager.instance.obstacles[PoolManager.instance.obstacles.Count - 1]);
                ObstacleScript tempObstacle = currentLevelObstacles[currentLevelObstacles.Count - 1];
                PoolManager.instance.obstacles.RemoveAt(PoolManager.instance.obstacles.Count - 1);

                tempObstacle.transform.SetParent(obstacleParent.transform);
                tempObstacle.transform.localPosition = new Vector3(float.Parse(obstaclePosition[0]), 2f, float.Parse(obstaclePosition[1])) + platformDefaultTransform * platformIndex;
                tempObstacle.transform.gameObject.SetActive(true);
            }

            platformIndex++;

        }

    }

    public void SetLevelVariables()
    {
        int collectablesCount = 0;
        GameManager.winGold = levelDatas[GameManager.levelNumber].winGold;
        GameManager.minimumGold = levelDatas[GameManager.levelNumber].minimumGold;

        /*foreach (var platform in levelDatas[GameManager.levelNumber].platforms)
        {
            collectablesCount += platform.collectables.Length;
        }
        GameManager.chakraFillValue = (float)100 / ((float)(collectablesCount*100));*/


    }

    public void SetTextValues()
    {

        UiManager.instance.levelText.text = "Level " + (GameManager.levelNumber + 1).ToString();
        UiManager.instance.goldText.text = GameManager.totalGold.ToString();
        UiManager.instance.winGoldText.text = GameManager.winGold.ToString();
        UiManager.instance.loseGoldText.text = GameManager.minimumGold.ToString();
    }

}

[System.Serializable]
public class LevelData
{
    public int winGold;
    public int minimumGold;
    [SerializeField] public Platform[] platforms;

}


[System.Serializable]
public class Platform
{
    [SerializeField] public Obstacle[] obstacles;


    [SerializeField] public Collectable[] collectables;
}

[System.Serializable]
public class Obstacle
{
    public string position;
    public float type;
}

[System.Serializable]
public class Collectable
{
    public string position;
    public float type;
}

