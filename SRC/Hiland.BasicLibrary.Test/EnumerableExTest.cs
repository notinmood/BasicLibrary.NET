using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary.Test
{
    [TestClass]
    public class EnumerableExTest
    {
         /// <summary>
         /// 测试两个集合的笛卡尔积
         /// </summary>
        [TestMethod]
        public void TestProduct()
        {
            List<string> list1 = ["beijing", "shanghai", "qingdao"];
            List<int> list2 = [1, 2, 3];
            var result = list1.Product(list2);

            //actual express
            var actual = result.Count();
            //expected express
            var expected = 9;
            // assert  
            Assert.AreEqual(expected, actual);

            //actual express
            var actualFirst = result.First();
            //expected express
            var expectedFirst = ("beijing", 1);
            // assert  
            Assert.AreEqual(expectedFirst, actualFirst);

            //actual express
            var actualLast = result.Last();
            //expected express
            var expectedLast = ("qingdao", 3);
            // assert  
            Assert.AreEqual(expectedLast, actualLast);
        }
    }
}