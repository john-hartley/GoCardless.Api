using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Integration.TestHelpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NeedsManualInterventionAttribute : ExplicitAttribute
    {
        public NeedsManualInterventionAttribute(string reason) : base(reason)
        {
        }
    }
}