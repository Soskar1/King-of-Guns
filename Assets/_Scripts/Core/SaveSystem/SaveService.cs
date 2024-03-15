using KingOfGuns.Core.Entities;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveService
    {
        private Player _player;
        private string _fileName;

        public SaveService(Player player, string fileName)
        {
            _player = player;
            _fileName = fileName;

            Regex regex = new Regex(".+\\.json$");
            if (!regex.Match(_fileName).Success)
                _fileName += ".json";
        }

        public void SaveToJson()
        {
            SaveData saveData = new SaveData(_player.transform.position);

            string fullPath = Path.Combine(Application.persistentDataPath, _fileName);
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(fullPath, json);
        }

        public SaveData LoadFromJson()
        {
            string fullPath = Path.Combine(Application.persistentDataPath, _fileName);
            string json = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}