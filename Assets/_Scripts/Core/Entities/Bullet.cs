using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : Entity
    {
        [SerializeField] private float _lifeTime;
        private ObjectPool<Bullet> _pool;
        private Timer _timer;
        private Coroutine _currentTimer;

        protected override void Awake() 
        {
            base.Awake();
            _timer = ServiceLocator.Instance.Get<Timer>();
        }

        public void Initialize(ObjectPool<Bullet> pool) => _pool = pool;

        public void OnEnable() => _currentTimer = _timer.StartTimer(_lifeTime, SendToPool);

        public void Update() => Move(Vector2.right);

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                return;

            _timer.StopTimer(_currentTimer);
            _currentTimer = null;
            SendToPool();
        }

        private void SendToPool() => _pool.Enqueue(this);
    }
}