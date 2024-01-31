using Microsoft.EntityFrameworkCore;
using QrCodeGenerator.Data;
using QrCodeGenerator.Models;

namespace QrCodeGenerator.Services;

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;

    public ContactService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _context.Contacts
            .Include(c => c.PhoneNumbers)
            .ToListAsync();
    }

    public async Task<Contact> GetContactByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.PhoneNumbers)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddContactAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<Contact> EditContact(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task UpdateContactAsync(int id, Contact contact)
    {
        var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

        existingContact.FirstName = contact.FirstName;
        existingContact.LastName = contact.LastName;
        existingContact.Emails = contact.Emails;
        existingContact.PhoneNumbers = contact.PhoneNumbers;
        existingContact.SocialMediaLinks = contact.SocialMediaLinks;

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.Include(c => c.PhoneNumbers).SingleOrDefaultAsync(c => c.Id == id);

        foreach (var phoneNumber in contact.PhoneNumbers)
        {
            _context.Remove(phoneNumber);
        }

        _context.Remove(contact);
        await _context.SaveChangesAsync();
    }

}