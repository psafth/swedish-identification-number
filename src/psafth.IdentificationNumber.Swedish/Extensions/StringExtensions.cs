using psafth.IdentificationNumber.Interfaces;
using psafth.IdentificationNumber.Swedish.Entities;
using System;


namespace psafth.IdentificationNumber.Swedish.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string value to a <see cref="PersonIdentificationNumber"/> or <see cref="BusinessRegistrationNumber"/>
        /// if the string value is matching any of them.
        /// </summary>
        /// <param name="value">The string value to match</param>
        /// <returns>Returning the matching object</returns>
        /// <exception cref="FormatException">When the string value is not matching either a <see cref="PersonIdentificationNumber"/> or <see cref="BusinessRegistrationNumber"/></exception>
        public static IIdentificationNumber ToIdentificationNumber(this string value)
        {
            if (PersonIdentificationNumber.TryParse(value, out var personIdentificationNumber))
                return personIdentificationNumber;
            else if (BusinessRegistrationNumber.TryParse(value, out var businessRegistrationNumber))
                return businessRegistrationNumber;

            throw new FormatException("Identification number given is not in a known format");
        }

        /// <summary>
        /// Convert the string to a given type of either <see cref="PersonIdentificationNumber"/> or <see cref="BusinessRegistrationNumber"/>.
        /// If the string is not matching the given type or if the type is not 
        /// </summary>
        /// <typeparam name="T">The type to cast to. Must be of <see cref="IdentificationNumber{T}"/> type.</typeparam>
        /// <param name="value">The string value to try to cast</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If the type if not of a <see cref="IdentificationNumber{T}"/> type then this exception is thrown</exception>
        public static T ToIdentificationNumber<T>(this string value) where T : IdentificationNumber<T>
        {
            if (!typeof(IdentificationNumber<T>).IsAssignableFrom(typeof(T)))
                throw new ArgumentException("The type given is not an IdentificationNumber type!");

            if (typeof(PersonIdentificationNumber).IsAssignableFrom(typeof(T)) && PersonIdentificationNumber.TryParse(value, out PersonIdentificationNumber personIdentificationNumber))
                return (T)Convert.ChangeType(personIdentificationNumber, typeof(T));
            else if (typeof(BusinessRegistrationNumber).IsAssignableFrom(typeof(T)) && BusinessRegistrationNumber.TryParse(value, out BusinessRegistrationNumber businessRegistrationNumber))
                return (T)Convert.ChangeType(businessRegistrationNumber, typeof(T));

            return default;
        }
    }
}
