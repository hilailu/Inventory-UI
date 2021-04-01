using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(PlayerController player)
    {
        PlayerData data = new PlayerData(player);
        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);
    }

    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/player.json";
        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            PlayerData pd = (PlayerData)serializer.Deserialize(file, typeof(PlayerData));
            return pd;
        }
    }
}
