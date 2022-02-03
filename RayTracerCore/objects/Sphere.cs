using RayTracerCore.materials;
using System;
using System.Collections.Generic;
using System.Text;
using Point = RayTracerCore.Vector;

namespace RayTracerCore.objects
{
    public class Sphere : Hittable
    {
        private Point center;

        private double radius;

        public Sphere(Point center, double radius, IMaterial material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
        }

        public Point Center { get { return this.center; } }

        public double Radius { get { return this.radius; } }

        public override HitRecord IsHit(Ray ray, double min, double max)
        {
            var oc = ray.Origin - center;
            var a = ray.Direction.LengthSquared();
            var halfB = Vector.Dot(oc, ray.Direction);
            var c = oc.LengthSquared() - radius * radius;

            var discriminant = halfB * halfB - a * c;

            HitRecord record = new HitRecord();
           
            if (discriminant < 0) { return record; }

            var sqrtd = Math.Sqrt(discriminant);
            var root = (-halfB - sqrtd) / a;
            if (root < min || root > max)
            {
                root = (-halfB + sqrtd) / a;
                if(root < min || root > max)
                {
                    return record;
                }
            }
             
            record.t = root;
            record.P = ray.GetAt(root);
            var normal = (record.P - center) / radius;
            var isFrontFace = Vector.Dot(ray.Direction, normal) < 0;
            record.Normal = (isFrontFace? normal : normal * -1).UnitVector();
            record.IsHit = true;
            record.ObjectHit = this;

            return record;
        }
    }
}
