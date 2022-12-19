using IdentificationNumber.Helpers;
using IdentificationNumber.Models;
using IdentificationNumber.Tests.Extensions;
using NuGet.Frameworks;

namespace IdentificationNumber.Tests
{
    [TestClass]
    public class BusinessRegistrationNumber_IsValid
    {
        [TestMethod]
        [DataRow("0604252387", true)]
        [DataRow("1702022383", true)]
        public void PartialYear_CurrentDecade_IsValid(string input, bool expected)
        {
            var personId = new PersonIdentificationNumber(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }

        [DataRow("201702022383", true)]
        [DataRow("200604292383", true)]
        public void FullYear_CurrentDecade_IsValid(string input, bool expected)
        {
            var personId = new PersonIdentificationNumber(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class BusinessRegistrationNumber_BusinessForm
    {
        [TestMethod]
        [DataRow("0604252387", true)]
        [DataRow("1702022383", true)]
        public void PartialYear_CurrentDecade_IsValid(string input, bool expected)
        {
            var personId = new PersonIdentificationNumber(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }

        [DataRow("201702022383", true)]
        [DataRow("200604292383", true)]
        public void FullYear_CurrentDecade_IsValid(string input, bool expected)
        {
            var personId = new PersonIdentificationNumber(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class BusinessRegistrationNumber_Constructor
    {
        [TestMethod]
        [DataRow("2120000142", "212000-0142")]
        [DataRow("212000-0142", "212000-0142")]
        [DataRow("556703-7485", "556703-7485")]
        [DataRow("802004-9642", "802004-9642")]
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
        [DataRow("191808019168", "180801+9168")]
        [DataRow("1702022383", "170202-2383")]
        public void FullYear_ToFormalString(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input).ToFriendlyName();

            Assert.AreEqual(expected, result);
        }
    }

}