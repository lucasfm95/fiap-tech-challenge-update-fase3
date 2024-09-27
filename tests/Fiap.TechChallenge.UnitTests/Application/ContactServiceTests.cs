using AutoFixture;
using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Application.Services;
using Fiap.TechChallenge.Domain.Entities;
using Fiap.TechChallenge.LibDomain.Events;
using Moq;

namespace TestProject1.Application;

public class ContactServiceTests
{
    [Fact]
    public async Task ShouldUpdateWithSuccess()
    {
        //Arrange
        var fixture = new Fixture();
        var message = fixture.Create<ContactUpdateEvent>();
        var contactRepository = new Mock<IContactRepository>();
        
        contactRepository.Setup(c => c.UpdateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var contactService = new ContactService(contactRepository.Object);

        //Act
        await contactService.UpdateAsync(message, CancellationToken.None);

        //Assert
        contactRepository.Verify(c =>
            c.UpdateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}