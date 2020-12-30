namespace GoCardlessApi.Common
{
    public static class Scheme
    {
        public static readonly string Ach = "ach";
        public static readonly string Autogiro = "autogiro";
        public static readonly string Bacs = "bacs";
        public static readonly string Becs = "becs";
        public static readonly string BecsNz = "becs_nz";
        public static readonly string Betalingsservice = "betalingsservice";
        public static readonly string Pad = "pad";

        /// <summary>
        /// Should only be used with the creditors endpoint.
        /// </summary>
        public static readonly string Sepa = "sepa";

        /// <summary>
        /// Can be used anywhere, except for the creditors endpoint.
        /// </summary>
        public static readonly string SepaCore = "sepa_core";

        /// <summary>
        /// Can be used anywhere, except for the creditors endpoint.
        /// <para>
        /// Note that, whilst not stated in the official API documentation, according to the GoCardless support team, the SEPA COR1 scheme has been deprecated - SEPA CORE should be used instead for all new data.
        /// </para>
        /// </summary>
        public static readonly string SepaCor1 = "sepa_cor1";
    }
}