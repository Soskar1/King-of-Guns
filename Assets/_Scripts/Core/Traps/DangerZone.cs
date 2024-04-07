using KingOfGuns.Core.Entities;
using KingOfGuns.Core.StageSystem;
using UnityEngine;

namespace KingOfGuns.Core.Traps
{
    public class DangerZone : MonoBehaviour
    {
        [SerializeField] private Level _level;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                _level.LoadSaveFile();
        }
    }
}