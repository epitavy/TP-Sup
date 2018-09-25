namespace EpitaSpaceProgram
{
    public class DamperSpring : Spring
    {
        private readonly Vector2 origin;
        private readonly double spring;
        private readonly double damping;

        private Vector2 previousPos;

        public DamperSpring(string name, double mass, double density, Vector2 initialPosition, Vector2 origin, double spring, double damping) 
            : base(name, mass, density, initialPosition, origin, spring)
        {
            this.origin = origin;
            this.spring = spring;
            this.damping = damping;
            previousPos = initialPosition;
        }

        public override void Update(double delta)
        {
            previousPos = Position;
            base.Update(delta);
            
            ApplyForce(-spring * (Position - origin));
            ApplyForce(-damping * (Position - previousPos) / delta);
        }
    }
}