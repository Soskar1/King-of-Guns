using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveService
    {
        private readonly string _fileName;
        private readonly string _saveFileLocation;

        public SaveService(string fileName, string saveFileLocation)
        {
            _fileName = fileName;
            _saveFileLocation = saveFileLocation;

            Regex regex = new Regex(".+\\.json$");
            if (!regex.Match(_fileName).Success)
                _fileName += ".json";
        }

        public void SaveToJson(Transform player, int stageID, string worldName)
        {
            SaveData saveData = new SaveData(player.position, stageID, worldName);

            string fullPath = Path.Combine(_saveFileLocation, _fileName);
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(fullPath, json);
        }

        public SaveData LoadFromJson()
        {
            try
            {
                string fullPath = Path.Combine(_saveFileLocation, _fileName);
                string json = File.ReadAllText(fullPath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (FileNotFoundException e)
            {
                Debug.LogWarning("File not found! Error message: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogWarning("An error occurred while loading the file: " + e.Message);
                return null;
            }
        }
    }
}