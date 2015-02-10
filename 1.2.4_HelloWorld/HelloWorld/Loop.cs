using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace HelloWorld
{
    class Loop : Rhino_Processing
    {

        // declare variables
        Point3d earth;
        Point3d moon;

        double distance;

        public override void Setup()
        {
            // write lines to console
            RhinoApp.WriteLine("Hello World.");

            // Text Formating
            RhinoApp.WriteLine(String.Format("{0} apples.", 3));

            RhinoApp.WriteLine(String.Format("{0} frames",frame_no));

            distance = 10.0;

            // make new point(s)
            earth = new Point3d(0.0, distance, 0.0);
            moon = new Point3d(0.0, earth.Y + 2.0, 0.0);

        }

        public override void Draw()
        {
            double radians = frame_no / 180.0 * Math.PI;

            earth.X = Math.Cos(radians) * distance;
            earth.Y = Math.Sin(radians) * distance;

            // add earth to document
            doc.Objects.AddPoint(earth);

            //moon speed ratio (orbit time = earth : 365.4 days, moon : 27.32days) 
            double moon_speed = 365.4 / 27.32;

            moon.X = earth.X + Math.Cos(moon_speed * radians) * 2.0;
            moon.Y = earth.Y + Math.Sin(moon_speed * radians) * 2.0;

            // add moon to document
            doc.Objects.AddPoint(moon);

        }

    }
}
