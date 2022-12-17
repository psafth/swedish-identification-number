using IdentificationNumber.Extensions;
using IdentificationNumber.Models;

var pvalue = "test";
//var identificationNumber = pvalue.ToIdentificationNumber();
var identificationNumber = pvalue.ToIdentificationNumber<PersonIdentificationNumber>();

Console.WriteLine(identificationNumber.ToFriendlyName());

var bvalue = "212000-0142";
var businessRegistrationNumber = bvalue.ToIdentificationNumber();
Console.WriteLine(businessRegistrationNumber.ToFriendlyName());

Console.ReadLine();