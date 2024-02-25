using UnityEngine;

namespace KingOfGuns.Core
{
    public class Spawner : MonoBehaviour, IService
    {
        [SerializeField] private Level _level;

        public T Spawn<T>(Object prefab) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab);

            if (obj is IReloadable reloadable)
                _level.Register(reloadable);

            return obj;
        }

        public T Spawn<T>(Object prefab, Vector2 position, Quaternion rotation) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab, position, rotation);

            if (obj is IReloadable reloadable)
                _level.Register(reloadable);

            return obj;
        }
    }
}