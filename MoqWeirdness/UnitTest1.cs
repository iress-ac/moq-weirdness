using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace MoqWeirdness
{
    public class Tests
    {
        /// <summary>
        /// Fails with:
        /// Moq.MockException : Tests.ITestInterface.M(["TestInput"]) invocation failed with mock behavior Strict. All invocations on the mock must have a corresponding setup.
        /// </summary>
        [Test]
        public void Test_Failure()
        {
            var sut = new Mock<ITestInterface>(MockBehavior.Strict);

            var input = new List<string> { "TestInput" };

            sut.Setup(r => r.M(input.Select(c => c)));

            sut.Object.M(input);
        }

        [Test]
        public void Test_Success()
        {
            var sut = new Mock<ITestInterface>(MockBehavior.Strict);

            var input = new List<string> { "TestInput" };

            var localEnumerable = input.Select(c => c);
            sut.Setup(r => r.M(localEnumerable));

            sut.Object.M(input);
        }

        public interface ITestInterface
        {
            void M(IEnumerable<string> input);
        }
    }
}