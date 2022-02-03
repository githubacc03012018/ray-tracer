using RayTracerCore.objects;
using System;
using System.Collections.Generic;
using System.Text;
using Color = RayTracerCore.Vector;

namespace RayTracerCore.materials
{
    public abstract class Material : IMaterial
    {
        public Color Albedo {get;set;}
         
        public Ray Scatter(HitRecord hitRecord)
        {
            return null;
        }
    }
}
