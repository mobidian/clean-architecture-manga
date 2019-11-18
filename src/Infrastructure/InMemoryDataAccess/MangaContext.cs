namespace Infrastructure.InMemoryDataAccess
{
    using System;
    using System.Collections.ObjectModel;
    using Domain.ValueObjects;

    public sealed class MangaContext
    {
        public MangaContext()
        {
            var entityFactory = new EntityFactory();
            Customers = new Collection<Customer>();
            Accounts = new Collection<Account>();
            Credits = new Collection<Credit>();
            Debits = new Collection<Debit>();

            var customer = new Customer(
                new ExternalUserId("github/ivanpaulovich"),
                new SSN("8608179999"),
                new Name("Ivan Paulovich"));
            var account = new Account(customer);
            var credit = account.Deposit(
                entityFactory,
                new PositiveMoney(800));
            var debit = account.Withdraw(
                entityFactory,
                new PositiveMoney(100));
            customer.Register(account);

            Customers.Add(customer);
            Accounts.Add(account);
            Credits.Add((Credit)credit);
            Debits.Add((Debit)debit);

            DefaultCustomerId = customer.Id;
            DefaultAccountId = account.Id;

            var secondCustomer = new Customer(
                new ExternalUserId("github/andrepaulovich"),
                new SSN("8408319999"),
                new Name("Andre Paulovich"));
            var secondAccount = new Account(secondCustomer);

            Customers.Add(secondCustomer);
            Accounts.Add(secondAccount);

            SecondCustomerId = secondCustomer.Id;
            SecondAccountId = secondAccount.Id;
        }

        public Collection<Customer> Customers { get; set; }

        public Collection<Account> Accounts { get; set; }

        public Collection<Credit> Credits { get; set; }

        public Collection<Debit> Debits { get; set; }

        public Guid DefaultCustomerId { get; }

        public Guid DefaultAccountId { get; }

        public Guid SecondCustomerId { get; }

        public Guid SecondAccountId { get; }
    }
}
