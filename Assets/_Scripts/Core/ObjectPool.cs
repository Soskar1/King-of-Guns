using System.Collections.Generic;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> _pool;
        private T _prefab;
        private Spawner _spawner;

        public ObjectPool(Spawner spawner, T prefab, int initialCapacity = 100)
        {
            _spawner = spawner;
            _prefab = prefab;
            _pool = new Queue<T>(initialCapacity);
        }

        public void Enqueue(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        public T Dequeue() 
        {
            T obj;

            if (_pool.Count > 0)
                obj = _pool.Dequeue();
            else
                obj = _spawner.Spawn<T>(_prefab);

            obj.gameObject.SetActive(true);
            return obj;
        }
    }
}