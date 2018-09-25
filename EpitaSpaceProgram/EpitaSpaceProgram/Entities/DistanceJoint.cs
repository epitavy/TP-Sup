using System;
using System.Collections.Generic;
using EpitaSpaceProgram.ACDC;

namespace EpitaSpaceProgram
{
    public class DistanceJoint : IEntity
    {
        public Vector2 origin;
        public Body body;

        public DistanceJoint(Vector2 origin, Body body)
        {
            this.origin = origin;
            this.body = body;
        }

        public void Update(double delta)
        {
            Vector2 dist = body.Position - origin;
            Vector2 velocity = body.Velocity;
            double mass = body.Mass;
            Vector2 contrainteForce = dist * (Vector2.Dot(-mass * body.Acceleration, dist) - Vector2.Dot(mass * velocity, velocity)) / Vector2.Dot(dist, dist);
            body.ApplyForce(contrainteForce);
            
            
        }

        // Don't change this method.
        public IEnumerable<string> Serialize()
        {
            return new List<string>();
        }
    }
}