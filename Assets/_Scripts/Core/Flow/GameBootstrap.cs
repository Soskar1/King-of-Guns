using UnityEngine;
using UnityEngine.SceneManagement;

namespace KingOfGuns.Core.Flow
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private int _fpsCap;
        [SerializeField] private int _screenWidth;
        [SerializeField] private int _screenHeight;
        [SerializeField] private bool _fullscreen;

        private void Awake()
        {
            Application.targetFrameRate = _fpsCap;
            Screen.SetResolution(_screenWidth, _screenHeight, _fullscreen);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}