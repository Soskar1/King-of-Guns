using KingOfGuns.Core.SaveSystem;
using UnityEngine;

namespace KingOfGuns.Core.Collectibles
{
    public class ShotgunShell : MonoBehaviour, IReloadable
    {
        [SerializeField] private SpriteRenderer _visual;
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private float _deactivatedStateTimer;
        [SerializeField] private Timer _timer;
        [SerializeField] private Level _level;
        private Coroutine _currentTimer = null;

        private void Start() => _level.Register(this);

        public void Deactivate()
        {
            _visual.enabled = false;
            _trigger.enabled = false;
            _currentTimer = _timer.StartTimer(_deactivatedStateTimer, Activate);
        }

        private void Activate()
        {
            if (_currentTimer != null)
                _timer.StopTimer(_currentTimer);

            _currentTimer = null;
            _visual.enabled = true;
            _trigger.enabled = true;
        }

        public void Reload() => Activate();
    }
}