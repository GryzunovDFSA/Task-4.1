using System.Collections.Generic;

namespace Task_4
{
    public class Triangle
    {
        public int id;
        public double a, b, c;
        public double area;
        public TriangleType type;
        public bool isValid;

        public Triangle(int id, double a, double b, double c, TriangleType type, double area, bool isValid)
        {
            this.id = id;
            this.a = a;
            this.b = b;
            this.c = c;
            this.type = type;
            this.area = area;
            this.isValid = isValid;
        }
        public Triangle(int id, double a, double b, double c, TriangleType type, double area)
        {
            this.id = id;
            this.a = a;
            this.b = b;
            this.c = c;
            this.type = type;
            this.area = area;
        }
        public int Id { get { return id; } set { id = value; } }

        public double A { get { return a; } set { a = value; } }

        public double B { get { return b; } set { b = value; } }

        public double C { get { return c; } set { c = value; } }

        public TriangleType Type { get { return type; } set { type = value; } }

        public double Area { get { return area; } set { area = value; } }

        public bool IsValidType { get { return isValid; } set { isValid = value; } }
    }
}
