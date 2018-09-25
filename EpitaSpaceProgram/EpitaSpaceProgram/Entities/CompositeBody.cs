using System.Collections.Generic;

namespace EpitaSpaceProgram
{
    public abstract class CompositeBody : Body
    {
        protected List<Body> child_body = new List<Body>();
        protected CompositeBody(string name, double mass, double density, Vector2 initialPosition)
            : base(name, mass, density, initialPosition)
        {
        }

        protected void Add(Body body)
        {
            child_body.Add(body);
            Velocity += body.Velocity;
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            foreach (Body b in child_body)
            {
                Acceleration += b.Acceleration;
                b.Update(delta);
                
            }
        }
    }
}