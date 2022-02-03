using System;
using System.Collections.Generic;
using System.Text;
using Point = RayTracerCore.Vector;

namespace RayTracerCore
{
    public class Ray
    {
        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point Origin { get; set; }
        public Vector Direction { get; set; }
        public Point GetAt(double t)
        {
            return Origin + (Direction * t);
        }
    }
}
