using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject player;

    private int numberLevel = 2;
    private GameObject map, character;
    private int currentLevel = 1;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    public void InitMap(int level)
    {
        currentLevel = level;
        if (currentLevel <= numberLevel)
        {
            Object map = Resources.Load($"Prefabs/Maps/Map_{currentLevel}");
            this.map = Instantiate(map, Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log("Max level!!!");
        }
    }

    public void InitPlayer(Transform transform)
    {
        Vector3 position = new Vector3(transform.position.x, 2.54f, transform.position.z);
        character = Instantiate(player, position, Quaternion.identity);
        camera.GetComponent<CameraFollow>().SetTarget(character.transform);
    }

    public void NextStage()
    {
        ResetLevel();
        InitMap(++currentLevel);
    }

    public void Replay()
    {
        ResetLevel();
        InitMap(currentLevel);
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetCoin();
        if (character != null)
        {
            Destroy(character);
        }
        if (map != null)
        {
            Destroy(map);
        }
    }
}
