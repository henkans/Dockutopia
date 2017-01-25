using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dockutopia.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dockutopia.Tests.Utils
{

    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void RemoveDockerFirstOccurrence_RemoveFirstDockerCommand()
        {
            // Arrange
            var input = "docker ps -a";
            var expected = "ps -a";

            //Act
            var result = StringHelper.RemoveDockerFirstOccurrence(input);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveDockerFirstOccurrence_DoNotRemoveOtherDockerInText()
        {
            // Arrange
            var input = "ps -a docker docker-ing-docker dockerworker";
            var expected = "ps -a docker docker-ing-docker dockerworker";

            //Act
            var result = StringHelper.RemoveDockerFirstOccurrence(input);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }


}
