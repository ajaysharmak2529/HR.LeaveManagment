﻿using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HR.LeaveManagement.Application.JsonConverters
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, DateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }

}
