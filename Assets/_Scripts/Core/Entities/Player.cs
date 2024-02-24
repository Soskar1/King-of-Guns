namespace KingOfGuns.Core.Entities
{
    public class Player : Entity
    {
        private Jumping _jumping;
        private GroundCheck _groundCheck;

        public Player(IMovable movement, Jumping jumping, GroundCheck groundCheck) : base(movement)
        {
            _jumping = jumping;
            _groundCheck = groundCheck;
        }

        public void Jump()
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }
    }
}