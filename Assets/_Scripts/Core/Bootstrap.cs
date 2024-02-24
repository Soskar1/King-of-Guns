using UnityEngine;

namespace KingOfGuns.Core
{
    public class Bootstrap : MonoBehaviour
    {
        public void Awake()
        {
            Input.Instance.Enable();
        }
    }
}
