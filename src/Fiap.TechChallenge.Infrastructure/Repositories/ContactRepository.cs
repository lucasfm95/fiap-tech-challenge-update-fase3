using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Domain.Entities;
using Fiap.TechChallenge.Infrastructure.Context;

namespace Fiap.TechChallenge.Infrastructure.Repositories;

public class ContactRepository(ContactDbContext dbContext) : IContactRepository
{
    public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken)
    {
        dbContext.Update(contact);
        await dbContext.SaveChangesAsync();
    }
}