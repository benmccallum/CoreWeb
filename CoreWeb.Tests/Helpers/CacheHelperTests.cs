using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CoreWeb.Helpers;
using System.Web;
using System.IO;
using System.Linq.Expressions;

namespace CoreWeb.Tests.Helpers
{
    [TestClass]
    public class CacheHelperTests
    {
        private const int cacheDuration = 1000000;

        [TestInitialize]
        public void TestInitialize()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
        }

        [TestMethod]
        public void WritesToCache()
        {
            Mock<ISomeService> someServiceMock = new Mock<ISomeService>();
            Expression<Func<int>> serviceMethod = () => someServiceMock.Object.SomeMethod();

            var result = CacheHelper.Get(serviceMethod, cacheDuration);
            var cacheResult = CacheHelper.Get(serviceMethod, cacheDuration);

            someServiceMock.Verify(m => m.SomeMethod(), Times.Once());
            Assert.AreEqual(result, cacheResult);
        }

        [TestMethod]
        public void WritesToCacheWithGenericArguments()
        {
            Mock<ISomeService> someServiceMock = new Mock<ISomeService>();
            Expression<Func<int>> serviceMethod = () => someServiceMock.Object.SomeGenericMethod<int, string>(It.IsAny<string>());

            var result = CacheHelper.Get(serviceMethod, cacheDuration);
            var cacheResult = CacheHelper.Get(serviceMethod, cacheDuration);

            var differentResult = CacheHelper.Get(() => someServiceMock.Object.SomeGenericMethod<int, bool>(It.IsAny<bool>()), cacheDuration);
            var differentResult2 = CacheHelper.Get(() => someServiceMock.Object.SomeGenericMethod<float, string>(It.IsAny<string>()), cacheDuration);

            someServiceMock.Verify(m => m.SomeGenericMethod<int, string>(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(result, cacheResult);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }
    }

    public interface ISomeService
    {
        int SomeMethod();

        T1 SomeGenericMethod<T1, T2>(T2 input);
    }

    public class SomeService : ISomeService
    {
        public int SomeMethod()
        {
            return 1;
        }

        public T1 SomeGenericMethod<T1, T2>(T2 input)
        {
            return default(T1);
        }
    }
}
