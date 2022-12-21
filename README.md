# swedish-identification-number

The all in one library for swedish identification numbers such as personal (personnummer), coordination (samordningsnummer) and business registration number (organisationsnummer).

![test](https://img.shields.io/badge/dynamic/xml?label=.NET%20version&query=//Project/PropertyGroup/TargetFramework&url=https%3A%2F%2Fraw.githubusercontent.com%2Fpsafth%2Fswedish-identification-number%2Fmain%2Fsrc%2FIdentificationNumber%2FIdentificationNumber.csproj)
![dotnet-status event parameter](https://github.com/psafth/swedish-identification-number/actions/workflows/dotnet.yml/badge.svg)
![codeql-status event parameter](https://github.com/psafth/swedish-identification-number/actions/workflows/codeql.yml/badge.svg)

## Usage

### Personal identification number (personnummer)

```C#

// Given an string input such as 197607012395 or 7607012395... or 760701-2395.. or 19760701-2395
string strVal = "197607012395";                     

// Use the extension methods ToIdentificationNumber to parse it as an identification number. Badly formatted strings will throw an exception.
PersonIdentificationNumber identificationNumber = strVal.ToIdentificationNumber();

// Check if the number is valid according to the Luhn algorithm.
bool isValid = identificationNumber.IsValid;        

// Get the persons gender (sex)
var gender = identificationNumber.Gender;

// Geth the persons date of birth
var birth = identificationNumber.DateOfBirth;

Console.WriteLine(identificationNumber);                    // Output: 197607012395
Console.WriteLine(identificationNumber.ToFormalString());   // Output: 760701-2395

```

### Coordination number (samordningsnummer)

```C#
string strVal = "5401642383";

PersonIdentificationNumber identificationNumber = strVal.ToIdentificationNumber();

bool isValid = identificationNumber.IsValid;

Console.WriteLine(identificationNumber);                                        // Output: 195401642383
Console.WriteLine(identificationNumber.ToFormalString());                       // Output: 540164-2383
Console.WriteLine(identificationNumber.DateOfBirth.ToString("yyyy-MM-dd"));     // Output: 1954-01-04
```

### Business registration number (orgisationsnummer)
```C#
string strVal = "2120000142";             // or 212000-0142

BusinessRegistrationNumber identificationNumber = strVal.ToIdentificationNumber();

Console.WriteLine(identificationNumber);                                        // Output: 2120000142
Console.WriteLine(identificationNumber.ToFormalString());                       // Output: 212000-0142
Console.WriteLine(identificationNumber.BusinessForm);                           // Output: GovernmentAgency

```

### Validation attributes
![GitHub issue/pull request detail](https://img.shields.io/github/issues/detail/state/psafth/swedish-identification-number/28?label=Status)

### Fluid validation
![GitHub issue/pull request detail](https://img.shields.io/github/issues/detail/state/psafth/swedish-identification-number/29?label=Status)

### Ready for Entity Framework
E.g. A simple customer entity where a customer can be either a private customer or a business customer.

The property IdentificationNumber can hold any type of identification number implementing the IIdentificationNumber interface. There is no need for any backing field or multiple properties.

```C#
public class Customer
{
    [Key]
    public Guid Id { get; set; }
    
    public IIdentificationNumber IdentificationNumber { get; set; }
    
    public CustomerType Type { get; set; }
    public string Name { get; set; }
}
```
With value conversion in EF:
```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .Entity<Customer>()
        .Property(e => e.IdentificationNumber)
        .HasConversion(
            v => v.ToString(),
            v => v.ToIdentificationNumber());
}
```

