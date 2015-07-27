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
        Point3d earth;  //型 変数名
        Point3d moon;   //上記earthと同様です

        public override void Setup()
        {
            earth = new Point3d(0.0, 12.0, 0.0);    //変数名 = new 型
            moon = new Point3d(0.0, 10.0, 0.0);     //上記earthと同様です
        }

        public override void Draw()
        {
            double distance = 10.0;
            
            earth.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;
            earth.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;

            moon.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;
            moon.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;
            
            doc.Objects.AddPoint(earth);    //今開いているRhinocerosのドキュメント（ファイル）に追加
                                            //doc.Objects.型（変数名）
            doc.Objects.AddPoint(moon);     //上記earthと同様です
        }
    }
}
