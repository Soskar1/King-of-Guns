using KingOfGuns.Core.Collectibles;
using KingOfGuns.Core.StageSystem;
using UnityEngine;
using System;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private IMovable _movement;
        private ObjectPool<Bullet> _pool;
        private Timer _timer;
        private Coroutine _currentTimer;
        private TrailRenderer _trailRenderer;

        public Action<Bullet> OnReset;

        private bool _initialized = false;

        private void Awake() 
        {
            _movement = GetComponent<IMovable>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        public void Initialize(ObjectPool<Bullet> pool, Timer timer)
        {
            OnReset = null;

            if (_initialized)
                return;

            _pool = pool;
            _timer = timer;

            _initialized = true;
            _currentTimer = _timer.StartTimer(_lifeTime, () => { Reset(); OnReset?.Invoke(this); });
        }

        public void OnEnable()
        {
            if (_initialized)
                _currentTimer = _timer.StartTimer(_lifeTime, () => { Reset(); OnReset?.Invoke(this); });
        }

        public void Update() => _movement.Move(Vector2.right);

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                return;

            if (collider.GetComponent<Bullet>() != null)
                return;

            if (collider.GetComponent<ShotgunShell>() != null)
                return;

            if (collider.GetComponent<Stage>() != null)
                return;

            Reset();
            OnReset?.Invoke(this);
        }

        public void Reset()
        {
            if (_currentTimer == null)
                return;

            _timer.StopTimer(_currentTimer);
            _currentTimer = null;
            _trailRenderer.Clear();
            _pool.Enqueue(this);
        }
    }
}