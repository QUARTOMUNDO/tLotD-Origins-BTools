using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public void SaveFile(string fileName = "SaveData")
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";

        FileStream fileStream;

        if (File.Exists(path))
        {
            fileStream = File.OpenWrite(path);
        }
        else
        {
            fileStream = File.Create(path);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, this);
        fileStream.Close();
    }

    public static T LoadFile<T>(string fileName = "SaveData") where T : class
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        FileStream fileStream;

        T data;

        if (File.Exists(path))
        {
            fileStream = File.OpenRead(path);
        }
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        data = (T)bf.Deserialize(fileStream);
        fileStream.Close();

        return data;

    }

    public static implicit operator bool(SaveData a) { return a != null; }

}


[System.Serializable]
public class CreatureEditorSaveData : SaveData
{
    public string defaultXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/Test_Default.xml";
    public string sourceXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/Test_Source.xml";
    public string exportXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/Test_Export.xml";
}
