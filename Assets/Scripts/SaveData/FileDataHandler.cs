using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
    }

    public Dictionary<MainGameManager.GameEnds, bool> Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Dictionary<MainGameManager.GameEnds, bool> loadedData = null;

        if (File.Exists(fullPath))
        {
            string dataToLoad = "";

            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            loadedData = JsonConvert.DeserializeObject<Dictionary<MainGameManager.GameEnds, bool>>(dataToLoad);
        }

        return loadedData;
    }

    public void Save(Dictionary<MainGameManager.GameEnds, bool> data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string dataToStore = JsonConvert.SerializeObject(data);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }
}
