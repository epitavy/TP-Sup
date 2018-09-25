using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EpitaSpaceProgram.ACDC;

namespace EpitaSpaceProgram
{
    public class System : IEntity
    {
        // Use this list to stores your `Body` objects.
        private readonly List<Body> _bodies;
        private readonly double CONST;

        
        public System(double g)
        {
            _bodies = new List<Body>();
            CONST = g;
        }


        public void Update(double delta)
        {
            int count = _bodies.Count;
            foreach (Body body in _bodies)
            {
                body.Update(delta);
            }
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    Body bi = _bodies[i];
                    Body bj = _bodies[j];
                    
                    Vector2 i_over_j = bj.Position - bi.Position;
                    double distance = Vector2.Dist(bi.Position, bj.Position);
                    i_over_j = i_over_j.Normalized();
                    Vector2 force = i_over_j * (CONST * bi.Mass * bj.Mass / (distance * distance));
                    bj.ApplyForce(- force);
                    bi.ApplyForce(force);
                }
            }
        }

        // Do not edit this method.
        public IEnumerable<string> Serialize()
        {
            return _bodies.SelectMany(body => body.Serialize());
        }

        public void Add(Body body)
        {
            _bodies.Add(body);
        }
    }
}