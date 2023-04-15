using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Task_4;


namespace UnitTestForTraingle
{
    [TestClass]
    public class TestTriangles
    {
        private List<Triangle> trueTriangles = new List<Triangle>
        {
            new Triangle (1, 14, 9, 14, TriangleType.Isosceles|TriangleType.Acute, 59.657, true),
            new Triangle (2, 2.3, 3.2, 4.2, TriangleType.Scalene|TriangleType.Obtuse, 5.071, true),
            new Triangle (3, 14, 18, 23, TriangleType.Scalene|TriangleType.Obtuse, 125.980, true),
            new Triangle (4, 9.14, 9.14, 9.14, TriangleType.Equilateral|TriangleType.Acute,36.174, true)
        };

        private List<Triangle> falseTriangles = new List<Triangle>
        {
            new Triangle (1, 14, 9, 14, TriangleType.Isosceles|TriangleType.Acute, 59.657, false),
            new Triangle (2, 2.3, 3.2, 4.2, TriangleType.Scalene|TriangleType.Obtuse, 5.071, false),
            new Triangle (3, 14, 18, 23, TriangleType.Scalene|TriangleType.Obtuse, 125.980, false),
            new Triangle (4, 9.14, 9.14, 9.14, TriangleType.Equilateral|TriangleType.Acute,36.174, false)
        };

        private List<Triangle> Triangles_2Invalid_2Valid = new List<Triangle>
        {
            new Triangle (1, 14, 9, 14, TriangleType.Scalene|TriangleType.Obtuse, 59.657),
            new Triangle (2, 2.3, 3.2, 4.2, TriangleType.Isosceles|TriangleType.Acute, 5.071),
            new Triangle (3, 14, 18, 23, TriangleType.Scalene|TriangleType.Obtuse, 125.980),
            new Triangle (4, 9.14, 9.14, 9.14, TriangleType.Equilateral|TriangleType.Acute,36.174)
        };

        private List<Triangle> Triangles_Invalid_Valid = new List<Triangle>
        {
            new Triangle (3, 14, 18, 23, TriangleType.Scalene|TriangleType.Acute, 0),
            new Triangle (4, 9.14, 9.14, 9.14, TriangleType.Equilateral|TriangleType.Acute,36.174)
        };

        private Mock<ITriangleProvider> mock;
        private ITriangleService triangleService;
        private ITriangleValidateService triangleValidateService;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<ITriangleProvider>();
            triangleService = new TriangleService();
            triangleValidateService = new TriangleValidateService(mock.Object, triangleService);
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_True()
        {
            mock.Setup(a => a.GetAll()).Returns(trueTriangles);
            var result = triangleValidateService.IsAllValid();

            Assert.IsTrue(trueTriangles[0].isValid);
            Assert.IsTrue(trueTriangles[1].isValid);
            Assert.IsTrue(trueTriangles[2].isValid);
            Assert.IsTrue(trueTriangles[3].isValid);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_False()
        {
            mock.Setup(a => a.GetAll()).Returns(falseTriangles);
            var result = triangleValidateService.IsAllValid();

            Assert.IsFalse(falseTriangles[0].isValid);
            Assert.IsFalse(falseTriangles[1].isValid);
            Assert.IsFalse(falseTriangles[2].isValid);
            Assert.IsFalse(falseTriangles[3].isValid);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TriangleProvider_Is1Valid_True()
        {
            mock.Setup(a => a.GetById(It.IsAny<int>())).Returns(trueTriangles[0]);
            var result = triangleValidateService.IsValid(1);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TriangleProvider_Is2Valid_False()
        {
            mock.Setup(a => a.GetById(It.IsAny<int>())).Returns(falseTriangles[1]);
            var result = triangleValidateService.IsValid(2);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_Invalid_Valid()
        {
            mock.Setup(a => a.GetAll()).Returns(Triangles_Invalid_Valid);
            var result = triangleValidateService.IsAllValid();

            Assert.IsTrue(Triangles_Invalid_Valid[0].isValid);
            Assert.IsFalse(Triangles_Invalid_Valid[1].isValid);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_2Invalid_2Valid()
        {
            mock.Setup(a => a.GetAll()).Returns(Triangles_2Invalid_2Valid);
            var result = triangleValidateService.IsAllValid();

            Assert.IsTrue(Triangles_2Invalid_2Valid[0].isValid);
            Assert.IsTrue(Triangles_2Invalid_2Valid[1].isValid);
            Assert.IsTrue(Triangles_2Invalid_2Valid[2].isValid);
            Assert.IsFalse(Triangles_2Invalid_2Valid[3].isValid);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TriangleProvider_IsValid_True_False_True()
        {
            mock.Setup(a => a.GetById(It.IsAny<int>())).Returns(trueTriangles[3]);
            Assert.IsTrue(triangleValidateService.IsValid(trueTriangles[3].Id));
            Assert.IsTrue(trueTriangles[3].IsValidType);

            var lastA = trueTriangles[3].a;
            trueTriangles[3].a = 45.234;
            Assert.IsFalse(triangleValidateService.IsValid(trueTriangles[3].Id));
            Assert.IsFalse(trueTriangles[3].IsValidType);

            trueTriangles[3].a = lastA;
            Assert.IsTrue(triangleValidateService.IsValid(trueTriangles[3].Id));
            Assert.IsTrue(trueTriangles[3].IsValidType);
        }

        [TestMethod]
        public void TriangleProvider_IsValid_False_True_False()
        {
            mock.Setup(a => a.GetById(It.IsAny<int>())).Returns(falseTriangles[0]);
            Assert.IsFalse(triangleValidateService.IsValid(falseTriangles[0].Id));
            Assert.IsFalse(falseTriangles[0].IsValidType);

            var lastArea = falseTriangles[0].area;
            falseTriangles[0].area = 8.800;
            Assert.IsTrue(triangleValidateService.IsValid(falseTriangles[0].Id));
            Assert.IsTrue(falseTriangles[0].IsValidType);

            falseTriangles[2].area = lastArea;
            Assert.IsFalse(triangleValidateService.IsValid(falseTriangles[0].Id));
            Assert.IsFalse(falseTriangles[0].IsValidType);
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_True_False_True()
        {
            mock.Setup(a => a.GetAll()).Returns(trueTriangles);
            Assert.IsTrue(triangleValidateService.IsAllValid());

            var lastA = trueTriangles[0].a;
            var lastA1 = trueTriangles[1].a;
            var lastA2 = trueTriangles[2].a;
            var lastA3 = trueTriangles[3].a;
            trueTriangles[0].a = trueTriangles[1].a = trueTriangles[2].a = trueTriangles[3].a = 0;
            Assert.IsFalse(triangleValidateService.IsAllValid());

            trueTriangles[0].a = lastA;
            trueTriangles[1].a = lastA1;
            trueTriangles[2].a = lastA2;
            trueTriangles[3].a = lastA3;
            Assert.IsTrue(triangleValidateService.IsAllValid());
        }

        [TestMethod]
        public void TriangleProvider_IsAllValid_False_True_False()
        {
            mock.Setup(a => a.GetAll()).Returns(falseTriangles);
            Assert.IsFalse(triangleValidateService.IsAllValid());

            var lastType0 = falseTriangles[0].type;
            var lastType1 = falseTriangles[1].type;
            var lastArea2 = falseTriangles[2].area;
            var lastA4 = falseTriangles[3].a;
            falseTriangles[0].type = TriangleType.Isosceles|TriangleType.Acute;
            falseTriangles[1].type = TriangleType.Scalene|TriangleType.Obtuse;
            falseTriangles[2].area = 8.800;
            Assert.IsTrue(triangleValidateService.IsAllValid());

            falseTriangles[0].type = lastType0;
            falseTriangles[1].type = lastType1;
            falseTriangles[2].area = lastArea2;
            falseTriangles[3].a = -32.235;
            Assert.IsFalse(triangleValidateService.IsAllValid());
        }
    }
}
