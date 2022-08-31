using bART_Task.Domain;
using bART_Task.Domain.Models;
using bART_Task.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.Services
{
    public class AllInOneService : IAllInOneService
    {
        private readonly bARTDBContext _context;

        public AllInOneService(bARTDBContext context)
        {
            _context = context;
        }

        public async Task CreateIncident(string accountName, string contactFirstName, string contactLastName, 
                                                    string contactEmail, string incidentDescription)
        {
            if (await IsExistingAccount(accountName) == false)
            {
                throw new KeyNotFoundException("Cannot create incident without account");
            }

            if (await IsExistingContact(contactEmail))
            {
                await UpdateContact(contactEmail, contactFirstName, contactLastName);
                await LinkContactToAccount(contactEmail, accountName);
            }
            else
            {
                await CreateContact(contactFirstName, contactLastName, contactEmail);
                await LinkContactToAccount(contactEmail, accountName);
            }

            var newIncident = new Incident()
            {
                Description = incidentDescription
            };

            await _context.Incidents.AddAsync(newIncident);
            await _context.SaveChangesAsync();

            var account = await GetAccountByName(accountName);

            account.IncidentName = newIncident.Name;

            await _context.SaveChangesAsync();
        }

        public async Task CreateAccount(string accountName, string contactEmail)
        {
            if (await IsExistingAccount(accountName))
            {
                throw new ArgumentException("Account already exists");
            }

            if (await IsExistingContact(contactEmail) == false)
            {
                throw new ArgumentException("Account cannot be created without contact");
            }

            var newAccount = new Account()
            {
                Name = accountName
            };

            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            await LinkContactToAccount(contactEmail, accountName);
        }

        public async Task CreateContact(string contactFirstName, string contactLastName, string contactEmail)
        {
            if (await IsExistingContact(contactEmail))
            {
                throw new ArgumentException("Contact already exists");
            }

            var newContact = new Contact()
            {
                FirstName = contactFirstName,
                LastName = contactLastName,
                Email = contactEmail
            };

            await _context.Contacts.AddAsync(newContact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContact(string contactEmail, string contactFirstName, string contactLastName)
        {
            var contact = await GetContactByEmail(contactEmail);

            if (contact == null)
            {
                throw new KeyNotFoundException("Such contact does not exist");
            }

            contact.FirstName = contactFirstName;
            contact.LastName = contactLastName;

            await _context.SaveChangesAsync();
        }

        public async Task LinkContactToAccount(string contactEmail, string accountName)
        {
            var contact = await GetContactByEmail(contactEmail);
            var account = await GetAccountByName(accountName);

            if (contact == null || account == null)
            {
                throw new ArgumentException("");
            }

            contact.AccountId = account.Id;

            await _context.SaveChangesAsync();
        }

        private async Task<Contact?> GetContactByEmail(string email)
        {
            var contact = await _context.Contacts
                .Where(contact => contact.Email.Equals(email))
                .SingleOrDefaultAsync();

            return contact;
        }

        private async Task<Account?> GetAccountByName(string accountName)
        {
            var account = await _context.Accounts
                .Where(account => account.Name.Equals(accountName))
                .SingleOrDefaultAsync();

            return account;
        }

        private async Task<bool> IsExistingAccount(string accountName)
        {
            return await _context.Accounts.Where(account => account.Name.Equals(accountName)).AnyAsync();
        }

        private async Task<bool> IsExistingContact(string email)
        {
            return await _context.Contacts.Where(account => account.Email.Equals(email)).AnyAsync();
        }
    }
}