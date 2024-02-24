using UnityEngine;

namespace KingOfGuns.Core
{
    public class Spawner : MonoBehaviour, IService
    {
        public Object Spawn(Object prefab) => Instantiate(prefab);
        public Object Spawn(Object prefab, Vector2 position, Quaternion rotation) => Instantiate(prefab, position, rotation);
    }
}