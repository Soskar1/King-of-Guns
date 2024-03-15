using UnityEngine;
using System.Collections.Generic;
using KingOfGuns.Core.SaveSystem;

namespace KingOfGuns.Core
{
    public class Level : MonoBehaviour
    {
        private List<IReloadable> _reloadables = new List<IReloadable>();
        private List<ISaveDataConsumer> _saveConsumers = new List<ISaveDataConsumer>();
        public SaveService SaveService { get; set; }

        public void Register(IReloadable reloadable)
        {
            if (!_reloadables.Contains(reloadable))
                _reloadables.Add(reloadable);
        }

        public void Register(ISaveDataConsumer consumer)
        {
            if (!_saveConsumers.Contains(consumer))
                _saveConsumers.Add(consumer);
        }

        public void Reload()
        {
            SaveData saveData = SaveService.LoadFromJson();
            _reloadables.ForEach(reloadable => reloadable.Reload());
            _saveConsumers.ForEach(consumer => consumer.ConsumeSave(saveData));
        }
    }
}