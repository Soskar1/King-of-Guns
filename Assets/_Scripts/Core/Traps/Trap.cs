using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Traps
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                Debug.Log("Trap killed a player!");
                player.Kill();
            }
        }
    }
}