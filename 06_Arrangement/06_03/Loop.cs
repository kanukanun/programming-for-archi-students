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

        public override void Setup() // this runs once in the beginning.


        {
            int a = 0;
            int b = 1;
            //２つの基準となる惑星を生成する。
            astroobjs.Add(new AstroObject(0, a, AstroObject.max_radius, new Point3d(0, 0, 0), 1496.0, 365.4));
            astroobjs.Add(new AstroObject(1, b, AstroObject.min_radius, new Point3d(0, 0, 0), 1496.0, 200.4));

            Random rnd1 = new Random();

            for (int i = 2; i < 10; i++)
            {
                double num = rnd1.Next(60, 300);
                double ro_days = rnd1.Next(60, 300);
                double ro_radius = rnd1.Next(3000);

                if (num > (AstroObject.max_radius + AstroObject.min_radius) / 2)
                {
                    if (num < AstroObject.max_radius)　//最大の半径の惑星が生まれたときに今度はそれを中心として衛星をつくる
                    {
                        astroobjs.Add(new AstroObject(i, a, num, astroobjs[a].Center(), astroobjs[a].radius + num + ro_radius, ro_days));
                    }
                    else
                    {
                        astroobjs.Add(new AstroObject(i, a, num, astroobjs[a].Center(), astroobjs[a].radius + num + ro_radius, ro_days));
                        a = i;
                    }
                }
                    else
                {
                    if (num > AstroObject.min_radius)　//最少の半径の惑星が生まれたときに今度はそれを中心として衛星をつくる
                    {
                        astroobjs.Add(new AstroObject(i, b, num, astroobjs[b].Center(), astroobjs[a].radius + num + ro_radius, ro_days));
                    }
                    else
                    {
                        astroobjs.Add(new AstroObject(i, b, num, astroobjs[b].Center(), astroobjs[a].radius + num + ro_radius, ro_days));
                        b = i;
                    }
                }
             }
        }


        public override void Draw()
        {
            foreach (AstroObject asa in astroobjs)
            {
                asa.Repel(astroobjs);
                asa.Display(doc);
            }

            astroobjs[0].Rotate();
            astroobjs[1].Rotate();
            
            for (int i = 2; i < astroobjs.Count; i++)
            {
                        astroobjs[i].UpdateCenter(astroobjs[astroobjs[i].parent_id].Center());
                        astroobjs[i].Rotate();
            }
        }
    }
}