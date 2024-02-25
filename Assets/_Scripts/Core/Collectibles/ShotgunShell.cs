using UnityEngine;

namespace KingOfGuns.Core.Collectibles
{
    public class ShotgunShell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _visual;
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private float _deactivatedStateTimer;
        private Timer _timer;

        private void Start() => _timer = ServiceLocator.Instance.Get<Timer>();

        public void Deactivate()
        {
            _visual.enabled = false;
            _trigger.enabled = false;
            _timer.StartTimer(_deactivatedStateTimer, Activate);
        }

        private void Activate()
        {
            _visual.enabled = true;
            _trigger.enabled = true;
        }
    }
}