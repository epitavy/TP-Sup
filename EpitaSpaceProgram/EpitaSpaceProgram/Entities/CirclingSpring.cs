﻿namespace EpitaSpaceProgram
{
    public class CirclingSpring : CompositeBody
    {
        public CirclingSpring(string name, double mass, double density, Vector2 initialPosition, Vector2 origin,
            double spring)
            : base(name, mass, density, initialPosition)
        {
            Vector2 initPos = initialPosition - origin;
            initPos = new Vector2(initPos.Y, -initPos.X) + origin;
            Add(new Spring("SpringX", mass, density, initialPosition, origin, spring));
            Add(new SpringMax("SpringY", mass, density, initPos, origin, spring));
        }
    }
}