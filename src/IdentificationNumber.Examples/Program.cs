using IdentificationNumber.Extensions;
using IdentificationNumber.Models;

Console.WriteLine("Known as person");
var validPersonInput1 = "1212121212";
var validPerson1 = validPersonInput1.ToIdentificationNumber<PersonIdentificationNumber>();

Console.WriteLine($"{validPerson1.ToFriendlyName()} | Valid: {validPerson1.IsValid} | Gender: {validPerson1.Gender} | Birth: {validPerson1.DateOfBirth}");

var validPersonInput2 = "121212+1212";
var validPerson2 = validPersonInput2.ToIdentificationNumber<PersonIdentificationNumber>();

Console.WriteLine($"{validPerson2.ToFriendlyName()} | Valid: {validPerson2.IsValid} | Gender: {validPerson2.Gender} | Birth: {validPerson2.DateOfBirth}");

Console.WriteLine("\nKnown as business");
var validBusinessInput1 = "212000-0142";
var validBusiness1 = validBusinessInput1.ToIdentificationNumber<BusinessRegistrationNumber>();
Console.WriteLine($"{validBusiness1.ToFriendlyName()} | Valid: {validBusiness1.IsValid} | Form: {validBusiness1.BusinessForm}");

var validBusinessInput2 = "5567037485";
var validBusiness2 = validBusinessInput2.ToIdentificationNumber<BusinessRegistrationNumber>();
Console.WriteLine($"{validBusiness2.ToFriendlyName()} | Valid: {validBusiness2.IsValid} | Form: {validBusiness2.BusinessForm}");


Console.WriteLine("\nUnknown");
var unknownInput1 = "1111111116";
var unknownNumberType1 = unknownInput1.ToIdentificationNumber();

Console.WriteLine($"{unknownNumberType1.ToFriendlyName()} | Valid: {unknownNumberType1.IsValid} | Type: {unknownNumberType1.GetType()}");


Console.WriteLine("\nUnknown but type checked and casted");
if (unknownNumberType1 is PersonIdentificationNumber)
{
    var knownType = unknownNumberType1 as PersonIdentificationNumber;
    Console.WriteLine($"{knownType.ToFriendlyName()} | Valid: {knownType.IsValid} | Gender: {knownType.Gender} | Birth: {knownType.DateOfBirth}");
}

if (unknownNumberType1 is BusinessRegistrationNumber)
{
    var knownType = unknownNumberType1 as BusinessRegistrationNumber;
    Console.WriteLine($"{knownType.ToFriendlyName()} | Valid: {knownType.IsValid} | Form: {knownType.BusinessForm}");
}





Console.ReadLine();