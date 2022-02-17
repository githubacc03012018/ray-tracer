using RayTracerCore.materials;
using RayTracerCore.objects;
using System;
using System.Diagnostics;
using System.IO;
using Color = RayTracerCore.Vector;
using Point = RayTracerCore.Vector;

namespace RayTracerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var aspectRatio = 16.0 / 9.0;
            int image_width = 1024;
            int image_height = (int)(image_width / aspectRatio);
            var origin = new Point(0, 0, 0);
              

            World world = new World();
            Hittable sphere1 = new Sphere(new Vector(0.0, 0.0, -1.0), 0.5, new Lambertian(new Color(0.8, 0.0, 0.0)));
            Hittable sphere2 = new Sphere(new Vector(-1.0, 0.0, -1.0), 0.5, new Lambertian(new Color(0.1, 0.6, 0.5)));
            Hittable ground = new Sphere(new Vector(0.0, -100.5, -1.0), 100.0, new Lambertian(new Color(0.8, 0.8, 0.0)));

            world.AddObjectToWorld(sphere1);
            world.AddObjectToWorld(sphere2);
            world.AddObjectToWorld(ground);


            Console.WriteLine("Working...");
            StreamWriter sw = new StreamWriter("image_aa4.ppm");
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.Out.WriteLine("P3\n");
            Console.Out.WriteLine(image_width + " " + image_height);
            Console.Out.WriteLine("255\n");

            var lookFrom = new Vector(3,3,2);
            var lookAt = new Vector(0, 0, -1);
            Camera cam = new Camera(20.0, aspectRatio, lookFrom, lookAt, 2.0, (lookFrom-lookAt).Length());
            RayTracer tracer = new RayTracer(cam);
            int depth = 20;
            var samples = 60;

            for (int j = image_height - 1; j >= 0; j--)
            {
                for (int i = 0; i < image_width; i++)
                {
                    Color color = new Color(0, 0, 0);
                    for (int s = 0; s < samples; s++)
                    {
                        double u = (double)(i + Vector.RandomDouble(-0.5, 0.5)) / (image_width - 1);
                        double v = (double)(j + Vector.RandomDouble(-0.5, 0.5)) / (image_height - 1);

                        //Ray ray = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
                        Ray ray = cam.GetRay(u, v);
                        color += tracer.RayTrace(ray, world, depth);
                    }

                    WriteColor(color, samples);
                }
            }
             

            Console.Out.Close();
            sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);

            Console.Out.WriteLine("Done");
        }

        public static double DegreesToRadians(double degrees)
        { 
            return degrees * Math.PI / 180.0;
        }

        static void WriteColor(Color color, int samples)
        {
            var r = color.GetX();
            var g = color.GetY();
            var b = color.GetZ();
            var scale = 1.0 / samples;

            r = Math.Sqrt(r * scale);
            g = Math.Sqrt(g * scale);
            b = Math.Sqrt(b * scale);

            Console.Out.WriteLine((int)(256 * Clamp(r, 0.0, 0.999)) + " " + (int)(256 * Clamp(g, 0.0, 0.999)) + " " + (int)(256 * Clamp(b, 0.0, 0.999)));
        }

        static double Clamp(double x, double min, double max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
        //static double HitSphere(Vector center, double radius, Ray ray)
        //{
        //    var oc = ray.Origin - center;
        //    var a = Vector.Dot(ray.Direction, ray.Direction);
        //    var b = 2.0 * Vector.Dot(oc, ray.Direction);
        //    var c = Vector.Dot(oc, oc) - radius * radius;
        //    var discriminant = b * b - 4 * a * c;
        //    if (discriminant < 0)
        //    {
        //        return -1.0;
        //    }
        //    else
        //    {
        //        return (-b - Math.Sqrt(discriminant)) / (2.0 * a);
        //    }
        //}

        //static Color ray_color(Ray ray)
        //{
        //    var t = HitSphere(new Vector(0, 0, -1), 0.5, ray);
        //    if (t > 0.0)
        //    {
        //        var N = (ray.GetAt(t) - new Vector(0, 0, -1)).UnitVector();
        //        return 0.5 * new Color(N.GetX() + 1, N.GetY() + 1, N.GetZ() + 1);
        //    }

        //    var unit_direction = ray.Direction.UnitVector();
        //    t = 0.5 * (unit_direction.GetY() + 1.0);
        //    return (1.0 - t) * new Color(1.0, 1.0, 1.0) + t * new Color(0.5, 0.7, 1.0);
        //}
    }
}
