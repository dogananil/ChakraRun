using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static List<LevelData> levelDatas = new List<LevelData>();

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
        //levelDatas[0].platforms[0].obstacles[0]
    }

}

[Serializable]
public class LevelData
{
    [SerializeField]
    public Platform[] platforms;

    public int winGold;
    public int minimumGold;
}


[Serializable]
public class Platform
{
    [SerializeField]
    public Obstacle[] obstacles;

    [SerializeField]
    public Collectable[] collectables;
}


[Serializable]
public class Obstacle
{
    public string position;
}


[Serializable]
public class Collectable
{
    public string position;
}
