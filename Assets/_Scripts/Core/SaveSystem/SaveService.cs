using System.IO;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public static class SaveService
    {
        private static string _fileName = "/kog.sav";

        public static void SaveToBinaryFile(SaveData saveData)
        {
            //byte[] bytes = MessagePackSerializer.Serialize(saveData);
            //File.WriteAllBytes(Application.persistentDataPath + _fileName, bytes);
        }
    }
}