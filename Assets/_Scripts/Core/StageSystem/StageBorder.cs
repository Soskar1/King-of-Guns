using KingOfGuns.Core.Entities;
using System;
using UnityEngine;

namespace KingOfGuns.Core.StageSystem
{
    public class StageBorder : MonoBehaviour
    {
        [SerializeField] private Stage _transitionTo;
        public Action PlayerTriggered;

        public Stage TransitionTo => _transitionTo;

        public void OnDisable() => PlayerTriggered = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
                PlayerTriggered?.Invoke();
        }
    }
}