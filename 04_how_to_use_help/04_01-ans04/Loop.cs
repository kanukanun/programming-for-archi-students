using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _04_01_template
{
    class Loop : Rhino_Processing
    {
        Point3d earth_center;
        Point3d moon_center;

        Sphere earth;
        Sphere moon;

        double distance = 1496.00;    //地球の公転半径

        public override void Setup()
        {
            earth_center = new Point3d(0.0, distance, 0.0);
            moon_center = new Point3d(0.0, earth_center.Y + 384.0, 0.0);

            earth = new Sphere(earth_center, 63.71);
            moon = new Sphere(moon_center, 17.37);
        }

        public override void Draw()
        {
            double radians = frame_no / 180.0 * Math.PI;

            earth_center.X = Math.Cos(radians) * distance;     //地球の軌道のX座標
            earth_center.Y = Math.Sin(radians) * distance;     //地球の軌道のY座標

            //moon speed ratio (orbit time = earth : 365.4 days, moon : 27.32days) 
            double moon_speed = 365.4 / 27.32;

            moon_center.X = earth_center.X + Math.Cos(moon_speed * radians) * 384.0;    //月の軌道のX座標
            moon_center.Y = earth_center.Y + Math.Sin(moon_speed * radians) * 384.0;    //月の軌道のY座標

            earth = new Sphere(new Point3d(earth_center.X, earth_center.Y, 0.0), 63.71);    //Sphereの（x,y,z）座標と半径
            moon = new Sphere(new Point3d(moon_center.X, moon_center.Y, 0.0), 17.37);   //Sphereの（x,y,z）座標と半径

            doc.Objects.AddSphere(earth);
            doc.Objects.AddSphere(moon);
        }
    }
}
