using System;
using System.Collections.Generic;
using System.Text;
using Point = RayTracerCore.Vector;

namespace RayTracerCore.objects
{
    public struct HitRecord
    {
        public double t { get; set; }
        public Vector Normal { get; set; }
        public Point P { get; set; }
        public Hittable ObjectHit { get; set; }
        public bool IsHit { get; set; }
    }
}
