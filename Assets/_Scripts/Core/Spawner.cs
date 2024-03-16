using UnityEngine;

namespace KingOfGuns.Core
{
    public class Spawner : MonoBehaviour
    {
        public T Spawn<T>(Object prefab) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab);
            return obj;
        }

        public T Spawn<T>(Object prefab, Vector2 position, Quaternion rotation) where T : MonoBehaviour
        {
            T obj = (T)Instantiate(prefab, position, rotation);
            return obj;
        }
    }
}