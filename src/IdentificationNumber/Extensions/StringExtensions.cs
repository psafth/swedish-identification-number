using IdentificationNumber.Helpers;
using IdentificationNumber.Interfaces;
using IdentificationNumber.Models;
using System;

namespace IdentificationNumber.Extensions
{
    public static class StringExtensions
    {
        public static IIdentificationNumber ToIdentificationNumber(this string value)
        {
            if (PersonIdentificationNumber.IsMatching(value))
                return new PersonIdentificationNumber(value);

            if (BusinessRegistrationNumber.IsMatching(value))
                return new BusinessRegistrationNumber(value);

            throw new FormatException("Identification number given is not in a known format");
        }

        public static T ToIdentificationNumber<T>(this string value) where T : IdentificationNumber
        {
            return Activator.CreateInstance(typeof(T),
                  value) as T;
        }
    }
}
