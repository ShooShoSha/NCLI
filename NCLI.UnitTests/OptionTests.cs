using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCLI.UnitTests
{
    [TestFixture]
    public class OptionTests : EqualityTestBase<Option>
    {
        #region Constant Tests
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
            StringAssert.StartsWith(Option.ARG_NAME, "arg");
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
        #endregion

        [Test]
        public void HasArguments_NumberOfArgumentsIsGreaterThanZero_ReturnTrue()
        {
            Option option = GetSimilarPrimary();
            int instances = 10;

            for (int i = 1; i < instances; i++)
            {
                option.NumberOfArguments = i;

                bool actual = option.HasArguments();

                Assert.IsTrue(actual);
            }
        }

        [Test]
        public void HasArguments_NumberOfArgumentsIsZero_ReturnFalse()
        {
            Option option = GetSimilarPrimary();
            option.NumberOfArguments = 0;

            bool actual = option.HasArguments();

            Assert.IsFalse(actual);
        }

        [Test]
        public void HasArguments_NumberOfArgumentsIsLessThanZero_ThrowsException()
        {
            Option option = GetSimilarPrimary();
            int instances = 10;

            for (int i = 1; i < instances; i++)
            {
                switch (i)
                {
                    case 1:
                    case 2:
                        break;
                    default:
                        var ex = Assert.Catch<ArgumentOutOfRangeException>(() => option.NumberOfArguments = -i);
                        StringAssert.Contains("below the minimum", ex.Message);
                        break;
                }
            }
        }

        [Test]
        public void HasArguments_NumberOfArgumentsIsUnlimited_ReturnTrue()
        {
            Option option = GetSimilarPrimary();
            option.NumberOfArguments = Option.UNLIMITED_VALUES;

            bool actual = option.HasArguments();

            Assert.IsTrue(actual);
        }

        [Test]
        public void HasArguments_NumberOfArgumentsIsUninitialized_ReturnFalse()
        {
            Option option = GetSimilarPrimary();

            bool actual = option.HasArguments();

            Assert.IsFalse(actual);
        }

        [Test]
        public void HasValueSeparator_DefaultSetup_ReturnsTrue()
        {
            Option option = GetSimilarPrimary();

            bool actual = option.HasValueSeparator();

            Assert.IsTrue(actual);
        }

        [Test]
        public void HasValueSeparator_Unset_ReturnsFalse()
        {
            Option option = GetSimilarPrimary();
            option.ValueSeparator = (char)0;

            bool actual = option.HasValueSeparator();

            Assert.IsFalse(actual);
        }

        [Test]
        public void OptionBuilder_Construction_UnspecifiedShortOption()
        {
            var ex = Assert.Catch<ArgumentException>(() => new Option.Builder("").Build());
            StringAssert.Contains("ShortOption or LongOption must be specified", ex.Message);
        }

        #region EqualityBaseTest Members
        protected override Option GetSimilarPrimary()
        {
            return new Option.Builder("?").Build();
        }

        protected override IEnumerable<Option> GetSimilarPrimaries()
        {
            int instances = 10;
            List<Option> similarPrimaries = new List<Option>(instances);
            for (int i = 0; i < instances; i++)
            {
                similarPrimaries.Add(GetSimilarPrimary());
            }
            return similarPrimaries;
        }

        protected override Option GetDifferentPrimary()
        {
            return new Option.Builder("v").Build();

        }

        protected override IEnumerable<Option> GetDifferentPrimaries()
        {
            string[] shortOptionLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            HashSet<string> shortOptions = new HashSet<string>(shortOptionLetters);
            List<Option> differentPrimaries = new List<Option>(shortOptions.Count);
            foreach (string shortOption in shortOptions)
            {
                Option differentPrimary = GetDifferentPrimary();
                differentPrimary.ShortOption = shortOption;
                differentPrimaries.Add(differentPrimary);
            }
            return differentPrimaries;
        } 
        #endregion
    }
}
