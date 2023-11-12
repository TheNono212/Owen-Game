using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HO
{
    public static class SaveSystem 
    {
        public static void SavePlayer(PlayerStats playerStats, PlayerLocomotion playerLocomotion)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.fun";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(playerStats, playerLocomotion);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.fun";
            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;

            }
            else{
                Debug.LogError("Save not found in " + path);
                return null;
            }
        }
    }
}