using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public ObstacleScript obstacle;
    public CollectableScript collectable;
    public PlatformScript platform;

    public List<ObstacleScript> obstacles = new List<ObstacleScript>();
    public List<CollectableScript> collectables = new List<CollectableScript>();
    public List<PlatformScript> platforms = new List<PlatformScript>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreatePool()
    {
        for (int i = 0; i < 30; i++)
        {
            ObstacleScript tempObstacle = Instantiate(obstacle, this.transform);
            tempObstacle.gameObject.SetActive(false);
            obstacles.Add(tempObstacle);
        }
        for (int i = 0; i < 30; i++)
        {
            CollectableScript tempCollectable = Instantiate(collectable, this.transform);
            tempCollectable.gameObject.SetActive(false);
            collectables.Add(tempCollectable);
        }
        for (int i = 0; i < 10; i++)
        {
            PlatformScript tempPlatform = Instantiate(platform, this.transform);
            tempPlatform.gameObject.SetActive(false);
            platforms.Add(tempPlatform);
        }
    }

    public void ResetPool()
    {
        foreach (var collectable in LevelManager.instance.currentLevelCollectables)
        {
            collectable.gameObject.SetActive(false);
            collectable.transform.SetParent(null);
            collectables.Add(collectable);
        }

        foreach (var obstacle in LevelManager.instance.currentLevelObstacles)
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.SetParent(null);
            obstacles.Add(obstacle);
        }
        foreach (var platform in LevelManager.instance.currentLevelPlatforms)
        {
            platform.gameObject.SetActive(false);
            platform.transform.SetParent(null);
            platforms.Add(platform);
        }

        LevelManager.instance.currentLevelCollectables.Clear();
        LevelManager.instance.currentLevelObstacles.Clear();
        LevelManager.instance.currentLevelPlatforms.Clear();
    }

}
