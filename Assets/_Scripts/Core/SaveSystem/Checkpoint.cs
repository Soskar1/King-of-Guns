using KingOfGuns.Core.Entities;
using KingOfGuns.Core.StageSystem;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class Checkpoint : MonoBehaviour, IStageObject
    {
        [SerializeField] private Level _level;
        private Stage _stageToSave;
        private bool _canSave = false;

        private void Awake() => _stageToSave = GetComponentInParent<Stage>();

        public void Disable() => _canSave = false;
        public void Enable() => _canSave = true;
        public void Reset() { }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (_canSave && collision.GetComponent<Bullet>() != null)
                _level.Save(_stageToSave.ID);
        }
    }
}