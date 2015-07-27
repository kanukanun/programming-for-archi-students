﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _03_01_template
{
    class Loop : Rhino_Processing
    {
        Point3d earth;  // 型 変数名;
        Point3d moon;   // 型 変数名;
        double distance = 10.0; //型 変数名（地球の公転半径） = 数値（固定）
        double DistanceBetweenMoonAndEarth = 2;      //型 変数名（地球と月の距離） = 数値（固定））
        double A = 13.4;    //　（地球の公転周期）÷（月の交点周期）

        public override void Setup()    //Setup関数
        {
            earth = new Point3d(0.0, distance, 0.0);    //最初の地球の位置を指定
            moon = new Point3d(0.0, distance + DistanceBetweenMoonAndEarth, 0.0);     //最初の月の位置を指定
        }

        public override void Draw()     //Draw関数
        {
            earth.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;      // frame_noに応じたearthのX座標を指定
            earth.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;      // frame_noに応じたearthのY座標を指定

            moon.X = earth.X + Math.Cos(frame_no / 180.0 * Math.PI * A);    // frame_noに応じたmoonのX座標を指定
            moon.Y = earth.Y + Math.Sin(frame_no / 180.0 * Math.PI * A);    // frame_noに応じたmoonのY座標を指定

            doc.Objects.AddPoint(earth);
            doc.Objects.AddPoint(moon);

            RhinoApp.WriteLine(String.Format("{0}", frame_no));
        }
    }
}
