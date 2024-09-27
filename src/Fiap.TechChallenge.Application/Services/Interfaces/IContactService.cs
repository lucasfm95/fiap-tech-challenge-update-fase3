using Fiap.TechChallenge.Domain.Entities;
using Fiap.TechChallenge.LibDomain.Events;

namespace Fiap.TechChallenge.Application.Services.Interfaces;

public interface IContactService
{
    Task<Contact> UpdateAsync(ContactUpdateEvent message, CancellationToken cancellationToken);
}