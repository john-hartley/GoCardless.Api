﻿using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ApiClientBase, ICreditorBankAccountsClient
    {
        public CreditorBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCreditorBankAccountRequest, CreateCreditorBankAccountResponse>(
                "creditor_bank_accounts",
                new { creditor_bank_accounts = request },
                idempotencyKey
            );
        }

        public Task<AllCreditorBankAccountsResponse> AllAsync()
        {
            return GetAsync<AllCreditorBankAccountsResponse>("creditor_bank_accounts");
        }

        public Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId)
        {
            return GetAsync<CreditorBankAccountResponse>($"creditor_bank_accounts/{creditorBankAccountId}");
        }

        public Task<DisableCreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request)
        {
            return PostAsync<DisableCreditorBankAccountRequest, DisableCreditorBankAccountResponse>(
                $"creditor_bank_accounts/{request.Id}/actions/disable"
            );
        }
    }
}