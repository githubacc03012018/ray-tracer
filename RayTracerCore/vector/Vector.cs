using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracerCore
{
    public class Vector
    {
        private readonly double[] e = new double[3];

        public Vector(double x, double y, double z)
        {
            this.e[0] = x;
            this.e[1] = y;
            this.e[2] = z;
        }

        public double GetX()
        {
            return this.e[0];
        }
        public double GetY()
        {
            return this.e[1];
        }
        public double GetZ()
        {
            return this.e[2];
        }
        public static double Dot(Vector v1, Vector v2)
        {
            return (v1.e[0] * v2.e[0]) + (v1.e[1] * v2.e[1]) + (v1.e[2] * v2.e[2]);
        }

        public static Vector Cross(Vector v1, Vector v2)
        {
            return new Vector(v1.e[1] * v2.e[2] - v1.e[2] * v2.e[1],
                                v1.e[2] * v2.e[0] - v1.e[0] * v2.e[2],
                                v1.e[0] * v2.e[1] - v1.e[1] * v2.e[0]);
        }

        public static Vector operator +(Vector v1, Vector v2)
                => new Vector(v1.e[0] + v2.e[0], v1.e[1] + v2.e[1], v1.e[2] + v2.e[2]);
         
        public static Vector operator -(Vector v1, Vector v2)
                 => new Vector(v1.e[0] - v2.e[0], v1.e[1] - v2.e[1], v1.e[2] - v2.e[2]);
        public static Vector operator *(Vector v1, Vector v2)
                => new Vector(v1.GetX() * v2.GetX(), v1.GetY() * v2.GetY(), v1.GetZ() * v2.GetZ());
        public static Vector operator *(Vector v1, double scalar)
                 => scalar * v1;

        public static Vector operator *(double scalar, Vector v1)
                => new Vector(v1.e[0] * scalar, v1.e[1] * scalar, v1.e[2] * scalar);

        public static Vector operator /(Vector v1, double scalar)
        {
            if (scalar == 0)
            {
                return v1;
            }

            return (1 / scalar) * v1;
        }
        public Vector UnitVector()
        {
            return this / this.Length();
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }
        public double LengthSquared()
        {
            return Math.Pow(this.e[0], 2) + Math.Pow(this.e[1], 2) + Math.Pow(this.e[2], 2);
        }

        public static Vector RandomVectorInUnitSphere()
        { 
            while(true)
            {
                Vector p = RandomUnitVector();
                if (p.LengthSquared() >= 1) continue;

                return p;
            }
        }

        public static Vector RandomInUnitDisk()
        {
            while (true)
            {
                var p = new Vector(RandomDouble(-1,1), RandomDouble(-1,1), 0);
                if (p.LengthSquared() >= 1)
                {
                    continue;
                }

                return p;
            }
        }

        public static Vector RandomUnitVector()
        {
            return new Vector(RandomDouble(-1.0, 1.0), RandomDouble(-1.0, 1.0), RandomDouble(-1.0, 1.0));
        }

        public static double RandomDouble(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
