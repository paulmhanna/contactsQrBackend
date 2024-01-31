using QrCodeGenerator.Models;

namespace QrCodeGenerator.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact> GetContactByIdAsync(int id);
    Task AddContactAsync(Contact contact);
    Task<Contact> EditContact(int id);
    Task UpdateContactAsync(int id, Contact contact);

    Task DeleteContactAsync(int id);
}