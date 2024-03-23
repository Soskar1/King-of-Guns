using UnityEngine;

namespace KingOfGuns.Core.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverText;

        public void Show() => _gameOverText.SetActive(true);
        public void Hide() => _gameOverText.SetActive(false);
    }
}