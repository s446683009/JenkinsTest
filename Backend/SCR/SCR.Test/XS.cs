using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Configuration;

namespace SCR.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
            var services = new ServiceCollection();
            
            services.AddSingleton();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}