using GoCardless.Api.Core.Serialisation;
using Newtonsoft.Json;
using System;

namespace GoCardless.Api.MandatePdfs
{
    public class CreateMandatePdfRequest
    {
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string BankCode { get; set; }
        public string Bic { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string DanishIdentityNumber { get; set; }
        public string Iban { get; set; }

        [JsonIgnore]
        public string Language { get; set; }

        public MandatePdfLinks Links { get; set; } 
        public string MandateReference { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }

        /// <summary>
        /// See <see cref="Models.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? SignatureDate { get; set; }

        public string SwedishIdentityNumber { get; set; }
    }
}