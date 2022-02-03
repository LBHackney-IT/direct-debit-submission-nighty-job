using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace DirectDebitSubmissionNightyJob.Helpers
{
    public class JsonValueConverter<TEntity> : ValueConverter<TEntity, string>
    {
        public JsonValueConverter(JsonSerializerOptions serializerSettings = null,
                                  ConverterMappingHints mappingHints = null)
            : base(model => JsonSerializer.Serialize(model, serializerSettings),
                   value => JsonSerializer.Deserialize<TEntity>(value, serializerSettings),
                   mappingHints)
        {
            //No ctor body; everything is passed through the call to base()
        }

#pragma warning disable CA1000 // Do not declare static members on generic types
        public static ValueConverter Default { get; } =
#pragma warning restore CA1000 // Do not declare static members on generic types
            new JsonValueConverter<TEntity>(null, null);

#pragma warning disable CA1000 // Do not declare static members on generic types
        public static ValueConverterInfo DefaultInfo { get; } =
#pragma warning restore CA1000 // Do not declare static members on generic types
            new ValueConverterInfo(typeof(TEntity),
                typeof(string),
                i => new JsonValueConverter<TEntity>(null, i.MappingHints));
    }
}
