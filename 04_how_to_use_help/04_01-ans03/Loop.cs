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
        Sphere earth;
        Sphere moon;
        Point3d earth_center;
        Point3d moon_center;
        double distance = 1496.00;    //地球の公転半径
        double distance_earth_moon = 384.00;    //月の公転半径
        double A = 13.4;    //（地球の公転周期）÷（月の公転周期）
        double R = 63.71;    //地球の半径
        double r = 17.37;   //月の半径

        public override void Setup()
        {
            earth_center = new Point3d(0.0, distance, 0.0);
            moon_center = new Point3d(0.0, distance + distance_earth_moon, 0.0);

            earth = new Sphere(earth_center, R);
            moon = new Sphere(moon_center, r);
        }

        public override void Draw()
        {
            earth_center.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;     //地球の軌道のX座標
            earth_center.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;     //地球の軌道のY座標

            moon_center.X = earth_center.X + Math.Cos(frame_no / 180.0 * Math.PI * A) * distance_earth_moon;    //月の軌道のX座標
            moon_center.Y = earth_center.Y + Math.Sin(frame_no / 180.0 * Math.PI * A) * distance_earth_moon;    //月の軌道のY座標

            earth = new Sphere(new Point3d(earth_center.X, earth_center.Y, 0.0), R);    //Sphereの（x,y,z）座標と半径
            moon = new Sphere(new Point3d(moon_center.X, moon_center.Y, 0.0), r);   //Sphereの（x,y,z）座標と半径

            doc.Objects.AddSphere(earth);
            doc.Objects.AddSphere(moon);
        }
    }
}
