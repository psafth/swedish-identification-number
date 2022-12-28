using psafth.IdentificationNumber.Swedish.Entities;

namespace psafth.IdentificationNumber.Swedish.Tests
{
    [TestClass]
    public class PersonIdentificationNumber_Gender
    {
        [TestMethod]
        [DataRow("194106279161")]
        [DataRow("195003072260")]
        public void Input_Female_IsTrue(string input)
        {
            var result = new PersonIdentificationNumber(input).Gender;
            Assert.AreEqual(Gender.Female, result);
        }

        [TestMethod]
        [DataRow("195809092652")]
        [DataRow("195811112217")]
        public void Input_Male_IsTrue(string input)
        {
            var result = new PersonIdentificationNumber(input).Gender;
            Assert.AreEqual(Gender.Male, result);
        }

        [TestMethod]
        [DataRow("195809092652")]
        [DataRow("195811112217")]
        public void Input_Female_IsFalse(string input)
        {
            var result = new PersonIdentificationNumber(input).Gender;
            Assert.AreNotEqual(Gender.Female, result);
        }

        [TestMethod]
        [DataRow("194106279161")]
        [DataRow("195003072260")]

        public void Input_Male_IsFalse(string input)
        {
            var result = new PersonIdentificationNumber(input).Gender;
            Assert.AreNotEqual(Gender.Male, result);
        }
    }

    [TestClass]
    public class PersonIdentificationNumber_ToFormalString
    {
        [TestMethod]
        [DataRow("191808019168", "180801+9168")]
        [DataRow("1702022383", "170202-2383")]
        public void FullYear_ToFormalString(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input).ToFormalString();

            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class PersonIdentificationNumber_IsValid
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
    public class PersonIdentificationNumber_Constructor
    {
        [TestMethod]
        [DataRow("1702052380", "201702052380")]
        [DataRow("2212133572", "202212133572")]
        public void Parse_PartialYear_CurrentDecade(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow("3104019140", "193104019140")]
        [DataRow("500307-2260", "195003072260")]
        public void Parse_PartialYear_PreviousDecade(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow("180801+9168", "191808019168")]
        public void Parse_PartialYear_PreviousDecade_OverHundred(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual, result.ToString());
        }

        [TestMethod]
        [DataRow("19180801+9168", "191808019168")]
        [DataRow("20180801+9168", "201808019168")]
        public void Parse_FullYear_CurrentDecade(string input, string expected)
        {
            var result = new PersonIdentificationNumber(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }
    }
}