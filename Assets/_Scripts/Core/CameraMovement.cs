using UnityEngine;

namespace KingOfGuns.Core
{
    public class CameraMovement : MonoBehaviour, IReloadable
    {
        [SerializeField] private BoxCollider2D _rightBorderCollider;
        [SerializeField] private BoxCollider2D _upBorderCollider;
        [SerializeField] private BoxCollider2D _leftBorderCollider;
        [SerializeField] private BoxCollider2D _downBorderCollider;
        [SerializeField] private float _offset;

        [SerializeField] private CameraPlayerDetection _rightBorder;
        [SerializeField] private CameraPlayerDetection _upBorder;
        [SerializeField] private CameraPlayerDetection _leftBorder;
        [SerializeField] private CameraPlayerDetection _downBorder;

        private Camera _camera;
        private float _height;
        private float _width;

        private void Awake()
        {
            _camera = Camera.main;

            _height = 2f * _camera.orthographicSize;
            _width = _height * _camera.aspect;

            _rightBorderCollider.offset = new Vector2(_width / 2 + _offset, 0);
            _rightBorderCollider.size = new Vector2(1, _height);

            _upBorderCollider.offset = new Vector2(0, _height / 2 + _offset);
            _upBorderCollider.size = new Vector2(_width, 1);

            _leftBorderCollider.offset = new Vector2(-_width / 2 - _offset, 0);
            _leftBorderCollider.size = new Vector2(1, _height);

            _downBorderCollider.offset = new Vector2(0, -_height / 2 - _offset);
            _downBorderCollider.size = new Vector2(_width, 1);

            _rightBorder.Subscribe(() => MoveCamera(Vector2.right));
            _upBorder.Subscribe(() => MoveCamera(Vector2.up));
            _leftBorder.Subscribe(() => MoveCamera(Vector2.left));
            _downBorder.Subscribe(() => MoveCamera(Vector2.down));
        }

        private void MoveCamera(Vector2 direction) => transform.position += new Vector3(direction.x * _width, direction.y * _height, 0);

        public void Reload() => transform.position = new Vector3(0, 0, -10);
    }
}