using System;
using System.Collections.Generic;
using System.Text;
using Point = RayTracerCore.Vector;
namespace RayTracerCore
{
    public class Camera
    {
        public Point LowerLeftCorner { get; set; }

        public Vector Horizontal { get; set; }

        public Vector Vertical { get; set; }

        public Point Origin { get; set; }

        public double LensRadius { get; set; }

        public Vector U { get; set; }

        public Vector V { get; set; }

        public Vector W { get; set; }


        public Camera(double fov, double aspectRatio, Point lookFrom, Point lookAt, double aperture, double focusDistance)
        {

            var theta = DegreesToRadians(fov);
            var h = MathF.Tan(theta / 2);
            var viewport_height = 2 * h;
            var viewport_width = aspectRatio * viewport_height;

            this.Origin = lookFrom;
            W = (lookFrom - lookAt).UnitVector();
            var vup = new Vector(0, 1, 0);

            U = Vector.Cross(vup, W).UnitVector();
            V = Vector.Cross(W, U).UnitVector();


            this.Vertical = focusDistance * viewport_height * V;
            this.Horizontal = focusDistance * viewport_width * U;

            LensRadius = aperture / 2;
            this.LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - focusDistance * W;
        }

        public Ray GetRay(double u, double v)
        {
            var rd = LensRadius * Vector.RandomInUnitDisk();
            var offset = U * rd.GetX() + V * rd.GetY();
            return new Ray(Origin + offset, LowerLeftCorner + u * Horizontal + v * Vertical - Origin - offset);
        }

        private float DegreesToRadians(double degrees)
        {
            return (float)degrees * MathF.PI / 180;
        }
    }
}
