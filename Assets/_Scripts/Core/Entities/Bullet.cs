using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : Entity
    {
        [SerializeField] private float _lifeTime;
        private float _timer;
        private ObjectPool<Bullet> _pool;

        public void Initialize(ObjectPool<Bullet> pool) => _pool = pool;

        public void OnEnable() => _timer = _lifeTime;

        public void Update()
        {
            Move(Vector2.right);

            if (_timer <= 0)
                _pool.Enqueue(this);
            else
                _timer -= Time.deltaTime;
        }
    }
}