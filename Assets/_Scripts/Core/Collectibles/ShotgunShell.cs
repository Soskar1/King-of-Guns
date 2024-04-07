using KingOfGuns.Core.StageSystem;
using UnityEngine;

namespace KingOfGuns.Core.Collectibles
{
    public class ShotgunShell : MonoBehaviour, IStageObject
    {
        [SerializeField] private SpriteRenderer _visual;
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private float _deactivatedStateTimer;
        [SerializeField] private Timer _timer;
        private Coroutine _currentTimer = null;

        public void Enable()
        {
            if (_currentTimer != null)
                _timer.StopTimer(_currentTimer);

            _currentTimer = null;
            _visual.enabled = true;
            _trigger.enabled = true;
        }

        public void Disable()
        {
            _visual.enabled = false;
            _trigger.enabled = false;
            _currentTimer = _timer.StartTimer(_deactivatedStateTimer, Enable);
        }

        public void Reset() { }
    }
}