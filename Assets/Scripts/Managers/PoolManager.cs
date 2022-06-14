using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public ObstacleScript obstacle_Type1;
    public ObstacleScript obstacle_Type2;
    public ObstacleScript obstacle_Type3;

    public CollectableScript collectable_Type1;
    public CollectableScript collectable_Type2;
    public CollectableScript collectable_Type3;

    public PlatformScript platform;
    public GameObject levelEnd;

    public List<ObstacleScript> obstacles_Type1 = new List<ObstacleScript>();
    public List<ObstacleScript> obstacles_Type2 = new List<ObstacleScript>();
    public List<ObstacleScript> obstacles_Type3 = new List<ObstacleScript>();

    public List<CollectableScript> collectables_Type1 = new List<CollectableScript>();
    public List<CollectableScript> collectables_Type2 = new List<CollectableScript>();
    public List<CollectableScript> collectables_Type3 = new List<CollectableScript>();

    public List<PlatformScript> platforms = new List<PlatformScript>();
    public GameObject levelEndObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            ObstacleScript tempObstacle = Instantiate(obstacle_Type1, this.transform);
            tempObstacle.gameObject.SetActive(false);
            obstacles_Type1.Add(tempObstacle);
        }
        for (int i = 0; i < 20; i++)
        {
            ObstacleScript tempObstacle = Instantiate(obstacle_Type2, this.transform);
            tempObstacle.gameObject.SetActive(false);
            obstacles_Type2.Add(tempObstacle);
        }
        for (int i = 0; i < 20; i++)
        {
            ObstacleScript tempObstacle = Instantiate(obstacle_Type3, this.transform);
            tempObstacle.gameObject.SetActive(false);
            obstacles_Type3.Add(tempObstacle);
        }

        for (int i = 0; i < 20; i++)
        {
            CollectableScript tempCollectable = Instantiate(collectable_Type1, this.transform);
            tempCollectable.gameObject.SetActive(false);
            collectables_Type1.Add(tempCollectable);
        }
        for (int i = 0; i < 20; i++)
        {
            CollectableScript tempCollectable = Instantiate(collectable_Type2, this.transform);
            tempCollectable.gameObject.SetActive(false);
            collectables_Type2.Add(tempCollectable);
        }
        for (int i = 0; i < 20; i++)
        {
            CollectableScript tempCollectable = Instantiate(collectable_Type3, this.transform);
            tempCollectable.gameObject.SetActive(false);
            collectables_Type3.Add(tempCollectable);
        }

        for (int i = 0; i < 10; i++)
        {
            PlatformScript tempPlatform = Instantiate(platform, this.transform);
            tempPlatform.gameObject.SetActive(false);
            platforms.Add(tempPlatform);
        }
        levelEndObject= Instantiate(levelEnd, this.transform);
        levelEnd.transform.position = Vector3.zero;
        levelEndObject.SetActive(false);
    }

    public void ResetPool()
    {
        foreach (var collectable in LevelManager.instance.currentLevelCollectables_Type1)
        {
            collectable.gameObject.SetActive(false);
            collectable.transform.SetParent(null);
            collectables_Type1.Add(collectable);
        }
        foreach (var collectable in LevelManager.instance.currentLevelCollectables_Type2)
        {
            collectable.gameObject.SetActive(false);
            collectable.transform.SetParent(null);
            collectables_Type2.Add(collectable);
        }
        foreach (var collectable in LevelManager.instance.currentLevelCollectables_Type3)
        {
            collectable.gameObject.SetActive(false);
            collectable.transform.SetParent(null);
            collectables_Type3.Add(collectable);
        }


        foreach (var obstacle in LevelManager.instance.currentLevelObstacles_Type1)
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.SetParent(null);
            obstacles_Type1.Add(obstacle);
        }
        foreach (var obstacle in LevelManager.instance.currentLevelObstacles_Type2)
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.SetParent(null);
            obstacles_Type2.Add(obstacle);
        }
        foreach (var obstacle in LevelManager.instance.currentLevelObstacles_Type3)
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.SetParent(null);
            obstacles_Type3.Add(obstacle);
        }

        foreach (var platform in LevelManager.instance.currentLevelPlatforms)
        {
            platform.gameObject.SetActive(false);
            platform.transform.SetParent(null);
            platforms.Add(platform);
        }

        levelEndObject.SetActive(false);
        levelEnd.transform.position = Vector3.zero;


        LevelManager.instance.currentLevelCollectables_Type1.Clear();
        LevelManager.instance.currentLevelCollectables_Type2.Clear();
        LevelManager.instance.currentLevelCollectables_Type3.Clear();

        LevelManager.instance.currentLevelObstacles_Type1.Clear();
        LevelManager.instance.currentLevelObstacles_Type2.Clear();
        LevelManager.instance.currentLevelObstacles_Type3.Clear();

        LevelManager.instance.currentLevelPlatforms.Clear();
        levelEnd.transform.position = Vector3.zero;
        levelEndObject.SetActive(false);
    }

}
