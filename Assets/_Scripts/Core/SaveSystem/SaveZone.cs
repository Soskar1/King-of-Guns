using KingOfGuns.Core.Entities;
using KingOfGuns.Core.StageSystem;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveZone : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private Stage _stageToSave;
        private BoxCollider2D _collider;

        private void Awake() => _collider = GetComponent<BoxCollider2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                _level.Save(_stageToSave.ID);
                _collider.enabled = false;
            }
        }
    }
}