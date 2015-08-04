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
        ///////////////
        //properties//
        //////////////
        
        private int id;
        public int parent_id;
        private double radius;
        private Point3d rotation_origin;
        private double rotation_radius;
        private double rotation_days;

        private Point3d position;
        private Sphere obj;

        private double direction = 1;
        private double days;
        private double passed_days = 1;
        private double collision_days;

        ////////////////
        //constructors//
        ////////////////
        
        public AstroObject(
            int _id,
            int _parent_id,
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
        }

        ///////////
        //methods//
        ///////////

        public void Rotate()
        {
            passed_days += 1;   //1フレーム毎にインクリメント
            //経過日数 = 衝突した日 + 回転方向 + 衝突した日からの経過日数
            days = collision_days + direction * passed_days;

            double X = rotation_origin.X + Math.Cos(days / rotation_days * Math.PI + 90) * rotation_radius;
            double Y = rotation_origin.Y + Math.Sin(days / rotation_days * Math.PI + 90) * rotation_radius;

            position = new Point3d(X, Y, 0);
        }

        public void Display(RhinoDoc _doc)
        {
            obj = new Sphere(position, radius);
            _doc.Objects.AddSphere(obj);
        }

        public Point3d Center()
        {
            return position;
        }

        public void UpdateCenter(Point3d _center)
        {
            rotation_origin = _center;
        }

        public void Reverse(List<AstroObject> astroobjs)
        {
            double sum_radius = 0;
            double distance = 0;
            for (int i = 0; i < astroobjs.Count; i++)
            {
                if (id != astroobjs[i].id)　//exclude oneself from an object
                {
                    sum_radius = radius + astroobjs[i].radius;　//total of the radius of oneself and the target planet
                    distance = position.DistanceTo(astroobjs[i].position);　//distance between the center

                    if (sum_radius > distance)
                    {
                            collision_days = days;//It stores a value when it collided
                            passed_days = 0;
                            direction = -direction;
                    }
                }
            }
        }

        //プログラム開始時に重なっているオブジェクトがあったらに数を進める
        public void Explosion(List<AstroObject> astroobjs)
        {
            double sum_radius = 0;
            double distance = 0;
            for (int i = 0; i < astroobjs.Count; i++)
            {
                if (id != astroobjs[i].id)　//exclude oneself from an object
                {
                    sum_radius = radius + astroobjs[i].radius;　//total of the radius of oneself and the target planet
                    distance = position.DistanceTo(astroobjs[i].position);　//distance between the center

                    if (sum_radius > distance)
                    {
                        passed_days += 30;
                    }
                }
            }
        }

        public void Transfer(List<AstroObject> objs) //if the nearest heavenly radius are bigger than one's planet, move!
        {
            int keep_parent_id = parent_id;　//keep parent id until the end
            double distance = 0;
            double min_distance = 0; //distance with the heavenly bodies nearby
            int min_id = parent_id; 

            for (int i = 0; i < objs.Count; i++)
            {
                if (id != i)　//exclude oneself
                {
                    distance = position.DistanceTo(objs[i].position);

                    if (min_distance == 0)
                    {
                        min_distance = distance;
                        min_id = i;
                    }
                    if (min_distance > distance)
                    {
                        min_distance = distance;
                        min_id = i;
                    }
                }
            }

            if (objs[parent_id].radius < objs[min_id].radius)
            {
                 parent_id = min_id;
            }

            int num = parent_id;
            int count = 0;

            while(new Point3d() != objs[num].position)
            {
                num = objs[num].parent_id;
                count++;

                if (count == objs.Count)
                {
                    parent_id = keep_parent_id;
                    break;
                }
            }
        }
    }
}
