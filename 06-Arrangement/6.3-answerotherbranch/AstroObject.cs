using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _5.Classes
{
    class AstroObject : Rhino_Processing 
    {
        //////////////
        //properties//
        //////////////

        public int id;
        public int parent_id;
        public double radius;
        public Point3d rotation_origin;
        public double rotation_radius;
        public double rotation_days;

        private Point3d position;
        private Sphere obj;
        //This function is a thing about the turns
        private double direction = 1;
        private double angle;
        private double sec = 1;
        private double collision;
        //This function sets maximum and the minimum of the radius
        static public double max_radius = 200;
        static public double min_radius = 100;

        /////////////////
        //constructors//
        ////////////////

        public AstroObject(
            int _id,
            int _parent_id,//id of the rotation origined planet 
            double _radius,
            Point3d _rotation_origin,
            double _rotation_radius,
            double _rotation_days
            )
        {

            // set variables
            id = _id;
            parent_id = _parent_id;
            radius = _radius;
            rotation_origin = _rotation_origin;
            rotation_radius = _rotation_radius;
            rotation_days = _rotation_days;

            // 生成された惑星の半径が最大・最少を超えたときそれを更新する。

            if (AstroObject.max_radius < _radius) {
                AstroObject.max_radius = _radius;
            }

            if (AstroObject.min_radius > _radius) {
                AstroObject.min_radius = _radius;
            }
        }


        //methods
        public void Rotate()
        {
            sec += 1;
            angle = collision + direction * sec;
            double X = rotation_origin.X + Math.Cos(angle / rotation_days * Math.PI) * rotation_radius;
            double Y = rotation_origin.Y + Math.Sin(angle / rotation_days * Math.PI) * rotation_radius;

            position = new Point3d(X, Y, 0);   
        }

        public void Display(RhinoDoc _doc)
        {
            obj = new Sphere(position, radius);
            _doc.Objects.AddSphere(obj);
        }

        //衝突時に公転方向を反転
        public void Repel(List<AstroObject> astroobjs)
        {
            double sum_radius = 0;
            double Distance = 0;
            for (int i = 0; i < astroobjs.Count; i++)
            {
                if (id == astroobjs[i].id)　//計測対象から自分を外す
                {
                }
                else
                {
                    sum_radius = radius + astroobjs[i].radius;　//自分と対象の惑星の半径の合計
                    Distance = position.DistanceTo(astroobjs[i].position);　//自分と相手との中心間の距離
                    if (sum_radius > Distance)
                    {
                        if (frame_no < 200)//開始時は惑星が重なっているためある程度回転してから行う
                        {
                        }
                        else
                        {
                            collision = angle;
                            sec = 0;
                            direction = -direction;
                        }

                    }
                    else
                    {
                    }
                }
            }
        }

        public Point3d Center()
        {
            return position;
        }

        public void UpdateCenter(Point3d _center)
        {
            rotation_origin = _center;
        }
    }
}
