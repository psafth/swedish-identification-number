using IdentificationNumber.Helpers;
using IdentificationNumber.Models;

namespace IdentificationNumber.Tests
{
    [TestClass]
    public class PersonIdentificationNumber_Constructor
    {
        [TestMethod]
        [DataRow("2212123572", true)]
        [DataRow("2212133572", true)]
        [DataRow("8907173572", true)]
        [DataRow("890717+3572", true)]
        public void Input_Valid_String(string input, bool expected)
        {
            var result = new PersonIdentificationNumber(input);
            Assert.AreEqual(expected, result);
        }
    }
}