using RayTracerCore.materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracerCore.objects
{
    public abstract class Hittable
    {
        public IMaterial material;

        public virtual HitRecord IsHit(Ray r, double tMin, double tMax)
        {
            return new HitRecord();
        }
    }
}
