using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core
{
    public class Input
    {
        public Controls Controls { get; private set; }
        public float MovementInput => Controls.Player.Movement.ReadValue<float>();
        public Vector2 ScreenMousePosition => Mouse.current.position.ReadValue();
        public Vector2 WorldMousePosition => Camera.main.ScreenToWorldPoint(ScreenMousePosition);

        public Input() => Controls = new Controls();
        public void Enable() => Controls.Enable();
        public void Disable() => Controls.Disable();
    }
}