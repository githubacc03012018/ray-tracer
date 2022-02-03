using RayTracerCore.objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracerCore.materials
{
    public interface IMaterial
    {
        Ray Scatter(HitRecord hitRecord);
    }
}
