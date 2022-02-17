using RayTracerCore.materials;
using RayTracerCore.objects;
using Color = RayTracerCore.Vector;

namespace RayTracerCore
{
    public class RayTracer
    {
        private Camera cam;
        public RayTracer(Camera cam) 
        { 
            this.cam = cam;
        }

        public Color RayTrace(Ray r, World world, int depth)
        {
            if (depth <= 0)
            {
                return new Color(0, 0, 0);
            }

            HitRecord hitRecord = world.IsHit(r, 0.0001, double.PositiveInfinity);
            if (hitRecord.IsHit)
            {
                var hitObject = hitRecord.ObjectHit;

                Ray scatteredRay = hitObject.material.Scatter(hitRecord);
                return ((Material)hitObject.material).Albedo * RayTrace(scatteredRay, world, depth - 1);
            }

            Vector unitDirection = r.Direction.UnitVector();
            double t = 0.5 * (unitDirection.GetY() + 1.0);


            return new Color(1.0, 1.0, 1.0) * (1.0 - t) + new Color(0.5, 0.7, 1.0) * t;
        }
    }
}
