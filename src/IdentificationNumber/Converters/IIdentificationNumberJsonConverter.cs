using IdentificationNumber.Extensions;
using IdentificationNumber.Interfaces;
using IdentificationNumber.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentificationNumber.Converters
{
    public class IdentificationNumberJsonConverter : JsonConverter<IIdentificationNumber>
    {
        public override IIdentificationNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetString().ToIdentificationNumber();


        public override void Write(Utf8JsonWriter writer, IIdentificationNumber value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToFormalString());
    }
}
