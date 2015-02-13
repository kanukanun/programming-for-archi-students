using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _4_how_to_use_help
{
    class Loop : Rhino_Processing
    {

        // declare variables
        
        // the origin, in other words the position of the sun
        Point3d origin;

        // two orbits;
        Sphere earth;
        Sphere moon;

        double earth_r,moon_r; // earth and moon radius 
        double sun_earth_distance,earth_moon_distance;

        public override void Setup()
        {

            origin = new Point3d(0.0,0.0,0.0);

            earth_r = 63.71; // actually its 6,371km
            moon_r = 17.37; 

       
            sun_earth_distance = 1496.00; // actually its 149,600,000km
            earth_moon_distance = 384.00; // actually its     384,000km

            // make new sphere
            earth = new Sphere(new Point3d(0.0,sun_earth_distance,0.0),earth_r);
            moon = new Sphere(new Point3d(0.0,sun_earth_distance+earth_moon_distance,0.0),moon_r);

        }

        public override void Draw()
        {

            // you need the up vector to determine the rotation axis 
            //Vector3d up_vector = new Vector3d(0.0,0.0,1.0);
            // this is the same as Vector3d.ZAxis, which always returns new Vector(0.0,0.0,1.0)
            
            // rotate the earth sphere (radians,rotation_axis,rotation_origin)
            double base_speed = 0.1;
            earth.Rotate(RhinoMath.ToRadians(base_speed), Vector3d.ZAxis, origin);

            // add earth to document
            doc.Objects.AddSphere(earth);

            //moon speed ratio (orbit time = earth : 365.4 days, moon : 27.32days) 
            double moon_speed = 365.4 / 27.32;

            // you can access to the spheres center by getting the .Center property.
            // rotation the moon sphere
            
            moon.Rotate(RhinoMath.ToRadians(base_speed * moon_speed), Vector3d.ZAxis, earth.Center);

            // add moon to document
            doc.Objects.AddSphere(moon);

        }

    }
}
