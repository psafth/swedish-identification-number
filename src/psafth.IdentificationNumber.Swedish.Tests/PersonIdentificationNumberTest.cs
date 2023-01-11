using psafth.IdentificationNumber.Entities;
using psafth.IdentificationNumber.Swedish.Entities;
using System;

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
            var result = PersonIdentificationNumber.Parse(input).Gender;
            Assert.AreEqual(Gender.Female, result);
        }

        [TestMethod]
        [DataRow("195809092652")]
        [DataRow("195811112217")]
        public void Input_Male_IsTrue(string input)
        {
            var result = PersonIdentificationNumber.Parse(input).Gender;
            Assert.AreEqual(Gender.Male, result);
        }

        [TestMethod]
        [DataRow("195809092652")]
        [DataRow("195811112217")]
        public void Input_Female_IsFalse(string input)
        {
            var result = PersonIdentificationNumber.Parse(input).Gender;
            Assert.AreNotEqual(Gender.Female, result);
        }

        [TestMethod]
        [DataRow("194106279161")]
        [DataRow("195003072260")]

        public void Input_Male_IsFalse(string input)
        {
            var result = PersonIdentificationNumber.Parse(input).Gender;
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
            var result = PersonIdentificationNumber.Parse(input).ToFormalString();

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
            var personId = PersonIdentificationNumber.Parse(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }

        [DataRow("201702022383", true)]
        [DataRow("200604292383", true)]
        public void FullYear_CurrentDecade_IsValid(string input, bool expected)
        {
            var personId = PersonIdentificationNumber.Parse(input);
            var result = personId.IsValid;

            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class PersonIdentificationNumber_Constructor
    {
        [TestMethod]
        [DataRow("0000000000")]
        [DataRow("ABCDEFGHIJ")]
        public void Parse_CheckInvalidInputs_ReturnsFormatException(string input)
        {
            Assert.ThrowsException<FormatException>(() => PersonIdentificationNumber.Parse(input));
        }

        [TestMethod]
        [DataRow("197002881237", "197002881237", PersonNumberType.Coordination)]  // Checks that the coordinationnumber is correct
        [DataRow("2212133572", "202212133572", PersonNumberType.Person)]          // Checks the type Person
        public void Parse_CheckInput_ReturnsExpected(string input, string expected, PersonNumberType expectedType)
        {
            var result = PersonIdentificationNumber.Parse(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual && expectedType == result.Type);
        }

        public void Parse_CheckIfNull_ReturnsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => PersonIdentificationNumber.Parse(null));
        }

        [DataRow("197002301236", "197002301236")]   // Check an invalid date even if Regex passes
        public void Parse_CheckInvalidDate_ReturnsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => PersonIdentificationNumber.Parse(null));
        }

        [TestMethod]
        [DataRow("1702052380", "201702052380")]
        [DataRow("2212133572", "202212133572")]
        public void Parse_PartialYear_CurrentDecade(string input, string expected)
        {
            var result = PersonIdentificationNumber.Parse(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow("3104019140", "193104019140")]
        [DataRow("500307-2260", "195003072260")]
        public void Parse_PartialYear_PreviousDecade(string input, string expected)
        {
            var result = PersonIdentificationNumber.Parse(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow("180801+9168", "191808019168")]
        public void Parse_PartialYear_PreviousDecade_OverHundred(string input, string expected)
        {
            var result = PersonIdentificationNumber.Parse(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual, result.ToString());
        }

        [TestMethod]
        [DataRow("19180801+9168", "191808019168")]
        [DataRow("20180801+9168", "201808019168")]
        public void Parse_FullYear_CurrentDecade(string input, string expected)
        {
            var result = PersonIdentificationNumber.Parse(input);
            var isEqual = result.Equals(expected);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        [DataRow(1960, 1, 1, Gender.Male, PersonNumberType.Person)]
        [DataRow(2018, 8, 1, Gender.Female, PersonNumberType.Person)]
        [DataRow(2000, 8, 1, Gender.Male, PersonNumberType.Coordination)]
        [DataRow(1958, 12, 31, Gender.Female, PersonNumberType.Coordination)]
        public void Create_CreatedPersonNumber_IsMatching(int year, int month, int day, Gender gender, PersonNumberType numberType)
        {
            DateTime yearMonthDay = new DateTime(year, month, day);

            var pin = PersonIdentificationNumber.Create(yearMonthDay, gender, numberType);

            System.Diagnostics.Trace.WriteLine($"Created PersonIdentificationNumber is '{pin}'");

            Assert.IsTrue(pin.IsValid && pin.Gender == gender && pin.Type == numberType);
        }

    }
}