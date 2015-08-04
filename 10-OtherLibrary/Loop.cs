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
        //Circleのリストを宣言
        List<Circle> circ = new List<Circle>(); 

        public override void Setup()
        {
            double xoff = 0;    //0から100のxの範囲で指定するため
            Perlin rnd = new Perlin();  //Perlinのインスタンスを生成する
            for (int i = 0; i < 100; i++)
            {
                double yoff = 100;  //100から110の範囲でyの引数を指定するため
                for (int j = 0; j <100; j++)
                {
                    //100×100の範囲にx,y座標それぞれ1の間隔で配置された点を中心とした円の半径をPerlinでを用いて決める
                    circ.Add(new Circle(new Point3d(i, j, 0), rnd.GetValue(xoff, yoff, 0) * -1));
                    yoff += 0.1;
                }
                xoff += 0.01;
            }
        }
        
        public override void Draw()
        {
            //全てのCircleを書き出していく
            for (int i = 0; i < circ.Count; i++)
            {
                doc.Objects.AddCircle(circ[i]);
            }
        }
    }
}
