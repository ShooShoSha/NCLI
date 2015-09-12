using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCLI.UnitTests
{
    [TestFixture]
    public abstract class EqualityTestBase<T>
    {
        [Test]
        public void Equals_Null_ReturnFalse()
        {
            T target = GetSimilarPrimary();

            bool actual = target.Equals(null);

            Assert.IsFalse(actual);
        }

        [Test]
        public void Equals_PassGenericObject_ReturnFalse()
        {
            T target = GetSimilarPrimary();
            object generic = new object();

            bool actual = target.Equals(generic);

            Assert.IsFalse(actual);
        }

        [Test]
        public void Equals_Symmetric_ReturnTrue()
        {
            T target = GetSimilarPrimary();
            T equivalent = GetSimilarPrimary();

            bool targetEqualsEquivalent = target.Equals(equivalent);
            bool equivalentEqualsTarget = equivalent.Equals(target);

            Assert.IsTrue(targetEqualsEquivalent);
            Assert.IsTrue(equivalentEqualsTarget);
        }

        [Test]
        public void Equals_Reflexive_ReturnTrue()
        {
            T target = GetSimilarPrimary();

            bool actual = target.Equals(target);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Equals_PassDifferent_ReturnFalse()
        {
            T target = GetSimilarPrimary();
            T different = GetDifferentPrimary();

            bool actual = target.Equals(different);

            Assert.IsFalse(actual);
        }

        [Test]
        public void Equals_RemainConsistent_ReturnTrue()
        {
            T previousTarget = GetSimilarPrimary();
            IEnumerable<T> targets = GetSimilarPrimaries();

            foreach (T target in targets)
            {
                bool actual = previousTarget.Equals(target);

                Assert.IsTrue(actual);
                previousTarget = target;
            }
        }

        [Test]
        public void Equals_RemainConsistent_ReturnFalse()
        {
            T previousTarget = GetDifferentPrimary();
            IEnumerable<T> targets = GetDifferentPrimaries();

            foreach (T target in targets)
            {
                bool actual = previousTarget.Equals(target);

                Assert.IsFalse(actual);
                previousTarget = target;
            }
        }

        [Test]
        public void Equals_IsTransitive_ReturnTrue()
        {
            T targetA = GetSimilarPrimary();
            T targetB = GetSimilarPrimary();
            T targetC = GetSimilarPrimary();

            bool targetAEqualsTargetB = targetA.Equals(targetB);
            bool targetBEqualsTargetC = targetB.Equals(targetC);
            bool targetAEqualsTargetC = targetA.Equals(targetC);

            Assert.IsTrue(targetAEqualsTargetB);
            Assert.IsTrue(targetBEqualsTargetC);
            Assert.IsTrue(targetAEqualsTargetC);
        }

        [Test]
        public void GetHashCode_SimilarObjects_ReturnTrue()
        {
            T target = GetSimilarPrimary();
            T similar = GetSimilarPrimary();

            bool actual = target.GetHashCode() == target.GetHashCode();

            Assert.IsTrue(actual);
        }

        [Test]
        public void GetHashCode_DifferentObjects_ReturnFalse()
        {
            T target = GetSimilarPrimary();
            T different = GetDifferentPrimary();

            bool actual = target.GetHashCode() == different.GetHashCode();

            Assert.IsFalse(actual);
        }

        [Test]
        public void GetHashCode_RemainsConsistent_MultipleInvocationsMatch()
        {
            T previousTarget = GetSimilarPrimary();
            IEnumerable<T> targets = GetSimilarPrimaries();

            foreach (T target in targets)
            {
                bool actual = previousTarget.GetHashCode() == target.GetHashCode();

                Assert.IsTrue(actual);
                previousTarget = target;
            }
        }

        [Test]
        public void GetHashCode_RemainsConsistent_MultipleDifferentInvocationsMismatch()
        {
            T previousTarget = GetDifferentPrimary();
            IEnumerable<T> targets = GetDifferentPrimaries();

            foreach (T target in targets)
            {
                bool actual = previousTarget.GetHashCode() == target.GetHashCode();

                Assert.IsFalse(actual);
                previousTarget = target;
            }
        }

        protected abstract T GetSimilarPrimary();
        protected abstract IEnumerable<T> GetSimilarPrimaries();
        protected abstract T GetDifferentPrimary();
        protected abstract IEnumerable<T> GetDifferentPrimaries();
    }
}
