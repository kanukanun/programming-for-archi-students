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
        List<AstroObject> astroobjs = new List<AstroObject>();

        public override void Setup()
        {
            astroobjs.Add(new AstroObject(63.71, new Point3d(), 1496.0, 365.4));

            Random rnd = new Random();

            for (int i = 1; i < 10; i++)
            {
                double rnd_days = rnd.Next(60, 300);

                astroobjs.Add(new AstroObject(17.37, astroobjs[0].Center(), 384.0 + i * 100, rnd_days));
            }
        }

        public override void Draw()
        {
            astroobjs[0].Rotate();
            astroobjs[0].Display(doc);

            for (int i = 1; i < astroobjs.Count; i++)
            {
                astroobjs[i].UpdateCenter(astroobjs[0].Center());
                astroobjs[i].Rotate();
                astroobjs[i].Display(doc);
            }
        }

    }
}