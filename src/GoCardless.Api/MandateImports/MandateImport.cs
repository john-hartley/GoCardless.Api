﻿using System;

namespace GoCardlessApi.MandateImports
{
    public class MandateImport
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// See <see cref="Common.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }

        public string Status { get; set; }
    }
}