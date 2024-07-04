using SimpleStringExcercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class Tests
    {
        [Fact]
        public void Case1()
        {
            var str = "  dfds   ";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("dfds", outStr);
        }

        [Fact]
        public void Case2()
        {
            var str = "dfds";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("dfds", outStr);
        }

        [Fact]
        public void Case3()
        {
            var str = "df ds";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("df*ds", outStr);
        }

        [Fact]
        public void Case4()
        {
            var str = "df  ds";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("df*ds", outStr);
        }

        [Fact]
        public void Case5()
        {
            var str = "   df  ds   ";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("df*ds", outStr);
        }

        [Fact]
        public void Case6()
        {
            string str = null;
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal(null, outStr);
        }

        [Fact]
        public void Case7()
        {
            string str = "";
            var stringService = new StringService();

            var outStr = stringService.ReplaceSpacesInString(str);

            Assert.Equal("", outStr);
        }
    }
}
