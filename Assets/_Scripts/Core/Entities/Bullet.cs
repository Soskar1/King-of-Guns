using KingOfGuns.Core.Collectibles;
using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : Entity
    {
        [SerializeField] private float _lifeTime;
        private ObjectPool<Bullet> _pool;
        private Timer _timer;
        private Coroutine _currentTimer;
        private TrailRenderer _trailRenderer;

        private bool _initialized = false;

        protected override void Awake() 
        {
            base.Awake();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        public void Initialize(ObjectPool<Bullet> pool, Timer timer)
        {
            if (_initialized)
                return;

            _pool = pool;
            _timer = timer;

            _initialized = true;
            _currentTimer = _timer.StartTimer(_lifeTime, SendToPool);
        }

        public void OnEnable()
        {
            if (_initialized)
                _currentTimer = _timer.StartTimer(_lifeTime, SendToPool);
        }

        public void Update() => Move(Vector2.right);

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                return;

            if (collider.GetComponent<Bullet>() != null)
                return;

            if (collider.GetComponent<ShotgunShell>() != null)
                return;

            Reload();
        }

        private void SendToPool() => _pool.Enqueue(this);

        public void Reload()
        {
            if (_currentTimer == null)
                return;

            _timer.StopTimer(_currentTimer);
            _currentTimer = null;
            _trailRenderer.Clear();
            SendToPool();
        }
    }
}