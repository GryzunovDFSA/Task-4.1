using System.Collections.Generic;

namespace Task_4
{
    public interface ITriangleProvider
    {
        Triangle GetById(int id);
        Triangle GetBySides(double a, double b, double c);
        List<Triangle> GetAll();
        void Save(Triangle triangle);
    }
}
