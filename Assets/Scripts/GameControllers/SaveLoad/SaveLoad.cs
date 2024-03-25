using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    public static void SavePlayer(PersonagemPrincipal player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.savefile";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerInfo info = new PlayerInfo(player);
    
        formatter.Serialize(stream, info);
        stream.Close();
    }

    public static PlayerInfo LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.savefile";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerInfo info = formatter.Deserialize(stream) as PlayerInfo;
            stream.Close();

            return info;
        }else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
