using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingOfGuns.Core.StageSystem
{
    public class Stage : MonoBehaviour
    {
        private List<IStageObject> _stageObjects = new List<IStageObject>();
        [SerializeField] private StageBorder _left;
        [SerializeField] private StageBorder _up;
        [SerializeField] private StageBorder _right;
        [SerializeField] private StageBorder _down;
        public Action<Stage> OnStageExit;
        private int _id;

        public int ID => _id;

        private void Awake()
        {
            IStageObject[] childrens = GetComponentsInChildren<IStageObject>();
            _stageObjects.AddRange(childrens);
            Disable();
        }

        public void Initialize(int id) => _id = id;

        private void OnEnable()
        {
            _left.PlayerTriggered += () => Exit(_left.TransitionTo);
            _right.PlayerTriggered += () => Exit(_right.TransitionTo);
            _up.PlayerTriggered += () => Exit(_up.TransitionTo);
            _down.PlayerTriggered += () => Exit(_down.TransitionTo);
        }

        public void Enable() => _stageObjects.ForEach(x => x.Enable());
        public void Disable() => _stageObjects.ForEach(_x => _x.Disable());
        public void Reload() => _stageObjects.ForEach(_x => _x.Reload());
        public void Exit(Stage transitionTo) => OnStageExit?.Invoke(transitionTo);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Camera camera = Camera.main;

            float _height = 2f * camera.orthographicSize;
            float _width = _height * camera.aspect;

            _height /= 2;
            _width /= 2;

            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, _height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, _height + transform.position.y));
        }
    }
}