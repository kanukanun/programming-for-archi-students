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
        //properties
        public double radius;
        public Point3d rotation_origin;
        public double rotation_radius;
        public double rotation_days;

        private Point3d position;
        private Sphere obj;

        // constructors
        public AstroObject(
            double _radius,
            Point3d _rotation_origin,
            double _rotation_radius,
            double _rotation_days
            )
        {

            // set variables
            radius = _radius;
            rotation_origin = _rotation_origin;
            rotation_radius = _rotation_radius;
            rotation_days = _rotation_days;

            // 
            

        }


        //methods
        public void Rotate()
        {
            double X = rotation_origin.X + Math.Cos(frame_no / rotation_days * Math.PI + 90) * rotation_radius;
            double Y = rotation_origin.Y + Math.Sin(frame_no / rotation_days * Math.PI + 90) * rotation_radius;

            position = new Point3d(X, Y, 0);   

            //RhinoApp.WriteLine(String.Format("{0}", frame_no));
            //RhinoApp.WriteLine(position1.ToString());
        }

        //public void Rotate2(RhinoDoc _doc)
        //{
        //    double X = Math.Cos(frame_no / 180.0 * Math.PI + 90) * rotation_radius;
        //    double Y = Math.Sin(frame_no / 180.0 * Math.PI + 90) * rotation_radius;

        //    double X1 = X + Math.Cos(frame_no / 180.0 * Math.PI + 90);
        //    double Y1 = Y + Math.Sin(frame_no / 180.0 * Math.PI + 90);

        //    Point3d position2 = new Point3d(X1, Y1, 0);

        //    RhinoApp.WriteLine(position2.ToString());

        //    _doc.Objects.AddPoint(position2);
        //}

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

    }
}