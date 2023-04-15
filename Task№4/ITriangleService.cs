using System.Collections.Generic;

namespace Task_4
{
    public interface ITriangleService
    {
        bool IsValidTriangle(double a, double b, double c);

        TriangleType GetType(double a, double b, double c);

        double GetArea(double a, double b, double c);

        void Save(double a, double b, double c, TriangleType type, double area);
    }
}
