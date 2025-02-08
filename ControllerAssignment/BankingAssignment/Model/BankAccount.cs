using System;

namespace BankingAssignment.Model;

public class BankAccount
{

    private readonly string accountNumber = "1001";
    private readonly string accountHolderName = "John Doe";

    private readonly float currentBalance = 5000;

    public class AccountDetailsType
    {
        public string? AccountNumber { get; set; }
        public string? AccountHolderName { get; set; }
        public float CurrentBalance { get; set; }
    }
    public AccountDetailsType AccountDetails
    {
        get => new AccountDetailsType
        {
            AccountNumber = accountNumber,
            AccountHolderName = accountHolderName,
            CurrentBalance = currentBalance
        };
    }


}
