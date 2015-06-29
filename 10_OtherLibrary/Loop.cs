using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Input;
using Rhino.Geometry;
using Rhino.Display;
using LibNoise;


namespace _5.Classes
{
    class Loop : Rhino_Processing
    {
        List<Circle> circ = new List<Circle>();

        public override void Setup()
        {
            double xoff = 0;
            Perlin rnd = new Perlin();
            for (int i = 0; i < 100; i++)
            {
                double yoff = 0;
                for (int j = 0; j <100; j++)
                {
                    circ.Add(new Circle(new Point3d(i, j, 0), rnd.GetValue(xoff, yoff, 0) * -1));
                    //RhinoApp.WriteLine(String.Format("{0}", circ[j].Radius));
                    yoff += 0.01;
                }
                xoff += 0.01;
            }
        }
        
        public override void Draw()
        {
            for (int i = 0; i < circ.Count; i++)
            {
                doc.Objects.AddCircle(circ[i]);
            }
        }
    }
}