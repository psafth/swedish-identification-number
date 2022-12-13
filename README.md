# swedish-identification-number

The all in one library for swedish identification numbers such as personal (personnummer), coordination (samordningsnummer) and business registration number (organisationsnummer).

## Features

### Personal identification number (personnummer)
[![feature-status-pid](https://img.shields.io/badge/Status-In%20progress-yellow.svg)](https://github.com/psafth/swedish-identification-number/issues/4)

```
string strVal = "197607012395";                             // Or 7607012395, or 760701-2395 or 19760701-2395

PersonIdentificationNumber identificationNumber = strVal.ToIdentificationNumber();

bool isValid = identificationNumber.IsValid;

Console.WriteLine(identificationNumber);                    // Output: 197607012395
Console.WriteLine(identificationNumber.ToFormalString());   // Output: 760701-2395

```

### Coordination number (samordningsnummer)
[![feature-status-pid](https://img.shields.io/badge/Status-In%20progress-yellow.svg)](https://github.com/psafth/swedish-identification-number/issues/4)

```
string strVal = "5401642383";

PersonIdentificationNumber identificationNumber = strVal.ToIdentificationNumber();

bool isValid = identificationNumber.IsValid;

Console.WriteLine(identificationNumber);                                        // Output: 195401642383
Console.WriteLine(identificationNumber.ToFormalString());                       // Output: 540164-2383
Console.WriteLine(identificationNumber.DateOfBirth.ToString("yyyy-MM-dd"));     // Output: 1954-01-04
```

### Business registration number (orgisationsnummer)
[![feature-status-pid](https://img.shields.io/badge/Status-In%20progress-yellow.svg)](https://github.com/psafth/swedish-identification-number/issues/5)
```
string strVal = "2120000142";             // or 212000-0142

BusinessRegistrationNumber identificationNumber = strVal.ToIdentificationNumber();
```

### Validation attributes
TBD

### Fluid validation
TBD

### Ready for Entity Framework
E.g. A simple customer entity where a customer can be either a private customer or a business customer.

The property IdentificationNumber can hold any type of identification number implementing the IIdentificationNumber interface. There is no need for any backing field or multiple properties.

```
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
```
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

