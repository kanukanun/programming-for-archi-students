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
        Point3d earth;  //型 変数名
        Point3d origin; //origin（変数）というポイントを宣言
        Line x;     //x（変数）というラインを宣言

        public override void Setup()
        {
            earth = new Point3d(0.0, 12.0, 0.0);    //変数名 = new 型
            origin = new Point3d(0, 0, 0);
            x = new Line(earth, origin);    //x(変数) = new Line(型)(始点,終点);
        }

        public override void Draw()
        {
            double distance = 10.0;
            
            earth.X = Math.Cos(frame_no / 180.0 * Math.PI) * distance;
            earth.Y = Math.Sin(frame_no / 180.0 * Math.PI) * distance;

            doc.Objects.AddPoint(earth);    //今開いているRhinocerosのドキュメント（ファイル）に追加
                                            //doc.Objects.型（変数名
            doc.Objects.AddLine(x);     //rhino上に変数xのラインを追加
        }
    }
}
