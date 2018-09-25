using System;
using EpitaSpaceProgram.ACDC;

namespace EpitaSpaceProgram.Scenes
{
    public class SolarSystemScene : Scene
    {
        public SolarSystemScene() : base(Math.Pow(10, -8), 60)
        {
            System system = new System(6.67408 * Math.Pow(10, -11));
            system.Add(new Body("Sun", 1.989e30, 1410, new Vector2(0, 0)));
            system.Add(new Body("Earth", 5.972e24, 5514, new Vector2(149.6e9, 0), new Vector2(0, -30e3)));
            system.Add(new Body("Moon", 7.34767309e22, 3344, new Vector2(149.6e9 + 384.4e6, 0),
                       new Vector2(0, -30e3 + 1022)));
            system.Add(new Body("Mars", 641.85e21, 3933, new Vector2(0, 227.9e6), new Vector2(-48e3, 0)));
            Add(system);
        }
    }
}