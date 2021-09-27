using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentService.API.IntegrationTest
{
    public class BasicTests
    {
        [Fact]
        public void Should_Be_True()
        {
            Assert.True(true);
        }

        [Fact]
        public void Should_be_False()
        {
            Assert.False(false);
        }
    }
}
