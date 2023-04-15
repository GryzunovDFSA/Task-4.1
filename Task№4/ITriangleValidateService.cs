using System.Collections.Generic;

namespace Task_4
{
    public interface ITriangleValidateService
    {
        bool IsAllValid();

        bool IsValid(int id);
    }
}
