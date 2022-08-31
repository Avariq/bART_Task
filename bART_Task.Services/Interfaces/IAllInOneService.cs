using bART_Task.Domain.Models;

namespace bART_Task.Services.Interfaces
{
    public interface IAllInOneService
    {
        Task CreateAccount(string accountName, string contactEmail);
        Task CreateContact(string contactFirstName, string contactLastName, string contactEmail);
        Task CreateIncident(string accountName, string contactFirstName, string contactLastName, string contactEmail, string incidentDescription);
        Task LinkContactToAccount(string contactEmail, string accountName);
        Task UpdateContact(string contactEmail, string contactFirstName, string contactLastName);
    }
}