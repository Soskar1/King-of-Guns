using KingOfGuns.Core.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KingOfGuns.Core
{
    public class NextLevelTeleport : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}