namespace KingOfGuns.Core
{
    public class Input
    {
        private static Input instance = null;
        public static Input Instance
        {
            get
            {
                if (instance == null)
                    instance = new Input();
                
                return instance;
            }
        }

        public Controls Controls { get; private set; }
        public float MovementInput => Controls.Player.Movement.ReadValue<float>();

        public Input() => Controls = new Controls();
        public void Enable() => Controls.Enable();
        public void Disable() => Controls.Disable();
    }
}