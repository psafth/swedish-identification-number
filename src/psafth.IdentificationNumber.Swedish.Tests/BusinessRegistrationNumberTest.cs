using psafth.IdentificationNumber.Swedish.Entities;

namespace psafth.IdentificationNumber.Swedish.Tests
{
    [TestClass]
    public class BusinessRegistrationNumber_IsValid
    {
        [TestMethod]
        [DataRow("2120000142")]
        public void Input_WithoutSeparator_IsValid_ReturnsTrue(string input)
        {
            var result = new BusinessRegistrationNumber(input).IsValid;
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("2120000143")]
        public void Input_WithoutSeparator_IsValid_ReturnsFalse(string input)
        {
            var result = new BusinessRegistrationNumber(input).IsValid;
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("212000-0142")]
        public void Input_WithSeparator_IsValid_ReturnsTrue(string input)
        {
            var result = new BusinessRegistrationNumber(input).IsValid;
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("212000-0143")]
        public void Input_WithSeparator_IsValid_ReturnsFalse(string input)
        {
            var result = new BusinessRegistrationNumber(input).IsValid;
            Assert.IsFalse(result);
        }

    }

    [TestClass]
    public class BusinessRegistrationNumber_BusinessForm
    {
        // TODO: Add test cases for business form
    }

    [TestClass]
    public class BusinessRegistrationNumber_Constructor
    {
        [TestMethod]
        [DataRow("2120000142", "2120000142")]
        [DataRow("212000-0142", "2120000142")]
        [DataRow("556703-7485", "5567037485")]
        [DataRow("802004-9642", "8020049642")]
        public void Parse_Valid_Input(string input, string expected)
        {
            var result = new BusinessRegistrationNumber(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow("121212-1212")]
        [DataRow("200604292391")]
        [DataRow("TestInput")]
        [DataRow("http://malicious.web/<script>alert(\"TEST\");</script>")]
        public void Input_Person_ThrowsFormatException(string input)
        {
            Action result = () => new BusinessRegistrationNumber(input);
            Assert.ThrowsException<FormatException>(result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("    ")]
        public void Input_Empty_ThrowsArgumentNullException(string input)
        {
            Action result = () => new BusinessRegistrationNumber(input);
            Assert.ThrowsException<ArgumentNullException>(result);
        }
    }

    [TestClass]
    public class BusinessRegistrationNumber_ToFormalString
    {
        [TestMethod]
        [DataRow("2120000142", "212000-0142")]
        public void Input_WithoutSeparator_ToFormalString(string input, string expected)
        {
            var result = new BusinessRegistrationNumber(input).ToFormalString();

            Assert.AreEqual(expected, result);
        }
    }
}