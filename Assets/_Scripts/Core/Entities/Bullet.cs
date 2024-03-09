using KingOfGuns.Core.Collectibles;
using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : Entity, IReloadable
    {
        [SerializeField] private float _lifeTime;
        private ObjectPool<Bullet> _pool;
        private Timer _timer;
        private Coroutine _currentTimer;
        private TrailRenderer _trailRenderer;

        protected override void Awake() 
        {
            base.Awake();
            _timer = ServiceLocator.Instance.Get<Timer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        public void Initialize(ObjectPool<Bullet> pool) => _pool = pool;

        public void OnEnable() => _currentTimer = _timer.StartTimer(_lifeTime, SendToPool);
        
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