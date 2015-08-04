using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _5.Classes
{
    class Loop : Rhino_Processing
    {
        AstroObject earth, moon; // declare our home planet and it's oribit.

        public override void Setup()
        {
            earth = new AstroObject(63.71, new Point3d(), 1496.0, 365.4);
            moon = new AstroObject(17.37, earth.Center(), 384.0, 27.32);

        }

        public override void Draw()
        {
            earth.Rotate();
            earth.Display(doc);
            // we need to update the center of the moon
            moon.UpdateCenter(earth.Center());

            moon.Rotate();
            moon.Display(doc);
        }

    }
}