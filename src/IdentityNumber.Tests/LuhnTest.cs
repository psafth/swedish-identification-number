using IdentityNumber.Helpers;

namespace IdentityNumber.Tests
{
    [TestClass]
    public class Luhn_GetControlNumber
    {
        [TestMethod]
        [DataRow("", 0)]
        [DataRow("180801916", 8)]
        [DataRow("840306239", 4)]
        [DataRow("060913239", 4)]
        [DataRow("121212121", 2)]
        public void Input_Is_String_ReturnValid(string input, int expected)
        {
            var result = Luhn.GetControlNumber(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }, 7)]
        [DataRow(new char[] { '1', '2', '1', '2', '1', '2', '1', '2', '1' }, 2)]
        public void Input_Is_CharArray_ReturnValid(IEnumerable<char> input, int expected)
        {
            var result = Luhn.GetControlNumber(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { }, 0)]
        [DataRow(new int[] { 0 }, 0)]
        [DataRow(new int[] { 1 }, 8)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 7)]
        [DataRow(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1 }, 2)]
        public void Input_Is_IntArray_ReturnValid(IEnumerable<int> input, int expected)
        {
            var result = Luhn.GetControlNumber(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("test")]
        public void Input_Is_Malformed_Throw(string input)
        {
            Action result = () => Luhn.GetControlNumber(input);
            Assert.ThrowsException<FormatException>(result);
        }
    }
}