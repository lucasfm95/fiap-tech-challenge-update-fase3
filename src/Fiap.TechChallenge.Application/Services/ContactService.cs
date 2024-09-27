using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.Domain.Entities;
using Fiap.TechChallenge.LibDomain.Events;

namespace Fiap.TechChallenge.Application.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public async Task<Contact> UpdateAsync(ContactUpdateEvent message, CancellationToken cancellationToken)
    {
        var contact = new Contact(message.Id, message.Name, message.Email, message.PhoneNumber, message.DddNumber);
        await contactRepository.UpdateAsync(contact,cancellationToken);
        return contact;
    }
}