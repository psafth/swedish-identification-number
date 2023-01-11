using psafth.IdentificationNumber.Interfaces;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace psafth.IdentificationNumber.Json
{
    public class IdentificationNumberJsonConverter<T> : JsonConverter<IdentificationNumber<T>>
    {
        //public override DateTimeOffset Read(
        //    ref Utf8JsonReader reader,
        //    Type typeToConvert,
        //    JsonSerializerOptions options) =>
        //        DateTimeOffset.ParseExact(reader.GetString()!,
        //            "MM/dd/yyyy", CultureInfo.InvariantCulture);

        public override IIdentificationNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.GetString();
            throw new NotImplementedException();
        }

        //public override void Write(
            //Utf8JsonWriter writer,
            //DateTimeOffset dateTimeValue,
            //JsonSerializerOptions options) =>
            //    writer.WriteStringValue(dateTimeValue.ToString(
            //        "MM/dd/yyyy", CultureInfo.InvariantCulture));

        public override void Write(Utf8JsonWriter writer, IIdentificationNumber value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
