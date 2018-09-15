﻿using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfRequest request);
    }
}