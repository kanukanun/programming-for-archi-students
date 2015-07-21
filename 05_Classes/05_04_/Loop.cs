using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _05_02_01_AstroObject
{
    class Loop : Rhino_Processing
    {
        AstroObject sun, venus, earth, moon, mars, jupiter, saturn; // declare our home planet and it's oribit.

        public override void Setup()
        {
            venus = new AstroObject(0, "venus", 60.51, new Point3d(), 1082.0, 224.4);
            earth = new AstroObject(1, "earth", 63.71, new Point3d(), 1496.0, 365.4);
            moon = new AstroObject(2, "moon", 17.37, earth.Center(), 384.0, 27.32);
            mars = new AstroObject(3, "mars", 33.97, new Point3d(), 2279.3, 686.5);
            jupiter = new AstroObject(4, "jupiter", 699.11, new Point3d(), 7785.0, 4329.5);
            saturn = new AstroObject(5, "saturn", 602.68, new Point3d(), 14267.2, 10752.9);
        }

        public override void Draw()
        {
            earth.Rotate();
            earth.Display(doc);
            venus.Rotate();
            venus.Display(doc);
            mars.Rotate();
            mars.Display(doc);
            jupiter.Rotate();
            jupiter.Display(doc);
            saturn.Rotate();
            saturn.Display(doc);
            // we need to update the center of the moon
            moon.UpdateCenter(earth.Center());

            moon.Rotate();
            moon.Display(doc);
        }

    }
}