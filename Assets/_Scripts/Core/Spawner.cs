using KingOfGuns.Core.SaveSystem;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Level _level;

        public T Spawn<T>(Object prefab) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab);

            TryRegisterToLevel(obj);

            return obj;
        }

        public T Spawn<T>(Object prefab, Vector2 position, Quaternion rotation) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab, position, rotation);

            TryRegisterToLevel(obj);

            return obj;
        }

        private void TryRegisterToLevel<T>(T obj)
        {
            if (obj is IReloadable reloadable)
                _level.Register(reloadable);

            if (obj is ISaveDataConsumer saveDataConsumer)
                _level.Register(saveDataConsumer);
        }
    }
}