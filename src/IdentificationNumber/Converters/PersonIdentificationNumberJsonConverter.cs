using IdentificationNumber.Extensions;
using IdentificationNumber.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentificationNumber.Converters
{
    public class PersonIdentificationNumberJsonConverter : JsonConverter<PersonIdentificationNumber>
    {
        public override PersonIdentificationNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetString().ToIdentificationNumber<PersonIdentificationNumber>();


        public override void Write(Utf8JsonWriter writer, PersonIdentificationNumber value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToFormalString());
    }
}
