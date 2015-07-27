using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _03_01_template
{
    class Loop : Rhino_Processing
    {
        Point3d earth;
        Point3d moon;   //課題1
        double distance = 10.0; //課題3
        double DistanceBetweenMoonAndEarth = 10.0 + 2;      //課題4 型 変数名（地球と月の距離） = 数値（固定）
        double A = 13.4;    //課題4　（地球の公転周期）÷（月の交点周期）

        public override void Setup()
        {
            earth = new Point3d(0.0, distance, 0.0);//課題3
            moon = new Point3d(0.0, distance + DistanceBetweenMoonAndEarth, 0.0);     //課題1
        }

        public override void Draw()
        {
            earth.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;
            earth.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;

            //moon.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;   //課題1
            //moon.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;   //課題1
            moon.X = earth.X + Math.Cos(frame_no / 180.0 * Math.PI * A);    //課題4 frame_noに応じたmoonのX座標を指定
            moon.Y = earth.Y + Math.Sin(frame_no / 180.0 * Math.PI * A);    //課題4 frame_noに応じたmoonのY座標を指定

            doc.Objects.AddPoint(earth);
            doc.Objects.AddPoint(moon);

            RhinoApp.WriteLine(String.Format("{0}", frame_no));     //課題2
        }
    }
}