using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Traps
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
            {
                Debug.Log("Trap killed a player!");
            }
        }
    }
}