using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataLoadManager : MonoBehaviour
{
    private static DataLoadManager instance;

    private Dictionary<MainGameManager.GameEnds, bool> endingsData;
    private string fileName = "saveddata.game";
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if ((instance != null) && (instance != this))
        {
            Debug.LogError("Instance of DataLoadManager has already been created!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static DataLoadManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Loadgame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void Loadgame()
    {
        endingsData = dataHandler.Load();

        if (endingsData != null)
        {
            MainGameManager.GetInstance().LoadData(endingsData);
        }
    }

    public void SaveGame()
    {
        MainGameManager.GetInstance().SaveData(ref endingsData);
        dataHandler.Save(endingsData);
    }

}
