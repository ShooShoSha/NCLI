using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCLI.UnitTests
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void Constant_Uninitialized_ReturnsConstantInteger()
        {
            Assert.AreEqual(-1, Option.UNINTIALIZED);
        }
        [Test]
        public void Constant_UnlimitedValues_ReturnsConstantInteger()
        {
            Assert.AreEqual(-2, Option.UNLIMITED_VALUES);
        }

        [Test]
        public void Constant_ValueSeparator_ReturnsConstantCharacter()
        {
            Assert.AreEqual('=', Option.VALUE_SEPARATOR);
        }

        [Test]
        public void Constant_ArgName_ReturnsConstantString()
        {
            Assert.AreEqual("arg", Option.ARG_NAME);
        }

        [Test]
        public void Constant_LongOptPrefix_ReturnsConstantString()
        {
            Assert.AreEqual("--", Option.LONG_OPT_PREFIX);
        }

        [Test]
        public void Constant_ShortOptPrefix_ReturnsConstantString()
        {
            Assert.AreEqual("-", Option.SHORT_OPT_PREFIX);
        }

        [Test]
        public void GetHashCode_OnlyShortOption_ReturnsInteger()
        {
            Option option = MakeShortOption();
            int actual = option.GetHashCode();
            int expected = "?".GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetHashCode_OnlyLongOption_ReturnsInteger()
        {
            Option option = MakeLongOption();
            int actual = option.GetHashCode();
            int expected = "help".GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        private Option MakeShortOption()
        {
            return new Option.Builder("?").Build();
        }

        private Option MakeLongOption()
        {
            return new Option.Builder("")
            {
                LongOption = "help"
            }.Build();
        }

        private Option MakeShortAndLongOption()
        {
            return new Option.Builder("?")
            {
                LongOption = "help"
            }.Build();
        }
    }
}
