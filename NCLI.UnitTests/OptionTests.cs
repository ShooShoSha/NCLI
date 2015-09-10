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

        private Option MakeOption()
        {
            return new Option("?", "Prints help message");
        }
    }
}
