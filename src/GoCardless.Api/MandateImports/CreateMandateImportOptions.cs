using Newtonsoft.Json;
using System;

namespace GoCardlessApi.MandateImports
{
    public class CreateMandateImportOptions
    {
        public CreateMandateImportOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        /// <summary>
        /// See <see cref="Common.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }
    }
}