

using psafth.IdentificationNumber.Swedish.Helpers;

namespace psafth.IdentificationNumber.Swedish.Tests
{
    [TestClass]
    public class Regex_MatchPerson
    {
        [TestMethod]
        [DataRow("9804302389")]             // Person born 1998-04-30
        [DataRow("980430-2389")]            // Person born 1998-04-30
        [DataRow("980430+2389")]            // Person born 1898-04-30
        [DataRow("200605162395")]           // Person born 2006-05-16
        [DataRow("20170202-2383")]          // Person born 2017-02-02
        [DataRow("5401762397")]             // Coordination born 1954-01-16
        [DataRow("195401762397")]           // Coordination born 1954-01-16
        [DataRow("200001632389")]           // Coordination born 2000-01-02
        [DataRow("000163+2389")]            // Coordination born 1900-01-02
        [DataRow(" 20000163-2389")]         // Resilience test for whitespaces
        [DataRow(" 20000163-2389 ")]        // Resilience test for whitespaces
        [DataRow(" 20000163 -2389 ")]       // Resilience test for whitespaces
        [DataRow(" 20000163 - 2389 ")]      // Resilience test for whitespaces
        [DataRow("  20000163  -  2389  ")]  // Resilience test for whitespaces
        public void String_MatchPerson_Successful(string input)
        {
            var result = CommonRegex.MatchPerson(input);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("    ")]
        [DataRow("test")]
        [DataRow("123456789")]
        [DataRow("2201702022383")]
        [DataRow("2201702022383\r\n")]
        [DataRow("220170\r\n2022383")]
        [DataRow("195401922397")]           // Coordination born 1954-01-32
        [DataRow("19540122397")]            // Invalid month
        public void String_MatchPerson_NotSuccessful(string input)
        {
            var result = CommonRegex.MatchPerson(input);
            Assert.IsFalse(result.Success);
        }
    }
}
