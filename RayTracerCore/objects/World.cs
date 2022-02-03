using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracerCore.objects
{
    public class World : Hittable
    {
        List<Hittable> objects = new List<Hittable>();

        public void AddObjectToWorld(Hittable obj)
        {
            this.objects.Add(obj);
        }

        public void ClearWorld()
        {
            this.objects.Clear();
        }

        public override HitRecord IsHit(Ray r, double tMin, double tMax)
        {
            var closestSoFar = tMax;
            HitRecord record = new HitRecord();

            foreach (var geometricObject in objects) {
                var objectHitRecord = geometricObject.IsHit(r, tMin, closestSoFar);
                if(objectHitRecord.IsHit)
                {
                    closestSoFar = objectHitRecord.t;
                    record = objectHitRecord;
                }
            }

            return record;
        }
    }
}
