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
            astroobjs.Add(new AstroObject(0, 0, 63.71, new Point3d(), 1496.0, 365.4));
            astroobjs.Add(new AstroObject(1, 1, 33.89, new Point3d(), 2279.3, 686.9));

            Random rnd = new Random();

            for (int i = 1; i < 10; i++)
            {
                double rnd_radius = rnd.Next(30, 100);
                double rnd_days = rnd.Next(60, 300);
                double rnd_distance = rnd.Next(3000);
                double ro_radius = rnd_radius;

                if (rnd_radius > 50)
                {
                    ro_radius += 63.71;
                    astroobjs.Add(new AstroObject(i, 0, rnd_radius, astroobjs[0].Center(), ro_radius + i * 100, rnd_days));
                }
                else
                {
                    ro_radius += 33.89;
                    astroobjs.Add(new AstroObject(i, 1, rnd_radius, astroobjs[1].Center(), ro_radius + i * 100, rnd_days));
                }
            }

            foreach (AstroObject objs in astroobjs)
            {
                objs.Explosion(astroobjs);
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < astroobjs.Count; i++)
            {
                if (astroobjs[i].parent_id == i)
                {
                    astroobjs[i].Rotate();
                }
                else
                {
                    int num = astroobjs[i].parent_id;
                    astroobjs[i].UpdateCenter(astroobjs[num].Center());
                    astroobjs[i].Rotate();
                }

                astroobjs[i].Transfer(astroobjs);
                astroobjs[i].Reverse(astroobjs);
                astroobjs[i].Display(doc);
            }
        }
    }
}