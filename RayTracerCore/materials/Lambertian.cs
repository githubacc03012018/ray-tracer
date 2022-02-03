using RayTracerCore.objects;
using System;
using System.Collections.Generic;
using System.Text;
using Color = RayTracerCore.Vector;

namespace RayTracerCore.materials
{
    public class Lambertian : Material, IMaterial
    {
        public Lambertian(Color albedo)
        {
            Albedo = albedo;
        }

        new public Ray Scatter(HitRecord hitRecord)
        {
            var normal = hitRecord.Normal;
            var point = hitRecord.P;

            var target = normal + point + Vector.RandomVectorInUnitSphere();

            return new Ray(point, target - point);
        }
    }
}
