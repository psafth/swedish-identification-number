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
            Assert.IsTrue(BusinessRegistrationNumber.TryParse(input, out _));
        }

        [TestMethod]
        [DataRow("2120000143")]
        public void Input_WithoutSeparator_IsValid_ReturnsFalse(string input)
        {
            Assert.IsFalse(BusinessRegistrationNumber.TryParse(input, out _));
        }

        [TestMethod]
        [DataRow("212000-0142")]
        public void Input_WithSeparator_IsValid_ReturnsTrue(string input)
        {
            Assert.IsTrue(BusinessRegistrationNumber.TryParse(input, out _));
        }

        [TestMethod]
        [DataRow("212000-0143")]
        public void Input_WithSeparator_IsValid_ReturnsFalse(string input)
        {
            Assert.IsFalse(BusinessRegistrationNumber.TryParse(input, out _));
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
            var result = BusinessRegistrationNumber.Parse(input);
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
            Assert.ThrowsException<FormatException>(() => BusinessRegistrationNumber.Parse(input));
        }

        [TestMethod]
        [DataRow(null)]
        public void Input_IsNull_ThrowsArgumentNullException(string input)
        {
            Assert.ThrowsException<ArgumentNullException>(() => BusinessRegistrationNumber.Parse(input));
        }
    }

    [TestClass]
    public class BusinessRegistrationNumber_ToFormalString
    {
        [TestMethod]
        [DataRow("2120000142", "212000-0142")]
        public void Input_WithoutSeparator_ToFormalString(string input, string expected)
        {
            var result = BusinessRegistrationNumber.Parse(input).ToFormalString();

            Assert.AreEqual(expected, result);
        }
    }
}