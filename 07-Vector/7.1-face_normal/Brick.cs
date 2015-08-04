using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino;
using Rhino.Geometry;

namespace _5.Classes
{
    class Brick
    {
        //////////////
        //properties//
        //////////////
        
        private int id;
        private double height;
        private double angle;

        private Point3d a , b , c , d , a1, b1, c1, d1;
        private NurbsSurface srf, srf1, srf2, srf3, srf4;

        ////////////////
        //constructors//
        ////////////////

        public Brick(
        int _id,
        double _height,
        double _angle
        )
        {
            id = _id;
            height = _height; 
            angle = _angle;
        }
        
        ///////////
        //methods//
        ///////////

        //一番目のSurfaceを作る
        public void MakeStartSrf()
        {
            a = new Point3d(0, 0, 0);
            b = new Point3d(100, 0, 0);
            c = new Point3d(100, 200, 0);
            d = new Point3d(0, 200, 0);

            srf = NurbsSurface.CreateFromCorners(a, b, c, d);
        }

        //上面を作る
        public void MakeSrf(List<Brick> brick)
        {
            for (int i = 0; i < id; i++)
            {
                //底面の法線ベクトル方向へ移動
                srf.Translate(brick[i].NormalVector() * height);
            }
        }

        //任意の3つの点でできる三角形の面積を求める
        private double TriangleArea(Point3d p0 , Point3d p1 , Point3d p2)
        {
            double s;
            double half;
            Line l1 , l2 , l3;

            //ヘロンの公式を使って面積を求める
            l1 = new Line(p0 , p1);
            l2 = new Line(p1 , p2);
            l3 = new Line(p2 , p0);
            half = (l1.Length + l2.Length + l3.Length) / 2;
            s = Math.Sqrt(half * (half - l1.Length) * (half - l2.Length) * (half - l3.Length));
            
            return s;
        }
        
        //Surfaceの法線ベクトルを求める
        private Vector3d NormalVector()
        {
            Vector3d normvec , ab , ad , cd , cb;
         
            //Surfaceの各頂点に点を生成
            a1 = srf.PointAt(0 , 0);
            b1 = srf.PointAt(100 , 0);
            c1 = srf.PointAt(100 , 200);
            d1 = srf.PointAt(0 , 200);

            //Surfaceを重なり合わない2つの三角形に分割し、それぞれ面積を求める
            double s1 = TriangleArea(a1 , b1 , d1);
            double s2 = TriangleArea(c1 , d1 , b1);

            double sum = s1 + s2;

            ab = new Vector3d(b1) - new Vector3d(a1);
            ad = new Vector3d(d1) - new Vector3d(a1);
            cd = new Vector3d(d1) - new Vector3d(c1);
            cb = new Vector3d(b1) - new Vector3d(c1);

            //任意の頂点に対する法線ベクトルを求め面積比をかけ足し合わせる
            normvec = Vector3d.CrossProduct(ab, ad) * (s1 / sum);
            normvec += Vector3d.CrossProduct(cd, cb) * (s2 / sum);
            
            return normvec / normvec.Length;
        }

        //底面の中心座標を求める
        private Point3d Center()
        {
            Point3d center = srf.PointAt(50 , 100);
            return center;
        }

        //Surfaceを回転させ積み上げていく
        public void MoveSrf(int num)//pile of brick
        {
            double ang = RhinoMath.ToRadians((angle / (num - 1)) * id);

            bool flag2 = srf.Rotate(ang , NormalVector(), Center());

            bool flag3 = srf.Rotate(ang, new Vector3d(0, 1, 0), Center());
        }
        
        public void Display(RhinoDoc _doc)
        {
            _doc.Objects.AddSurface(srf);
        }

    }
}
