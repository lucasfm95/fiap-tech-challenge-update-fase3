using AutoFixture;
using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.Domain.Entities;
using Fiap.TechChallenge.LibDomain.Events;
using Fiap.TechChallenge.Worker.Consumers;
using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject1.Worker;

public class ContactUpdateConsumerTests
{
    private readonly Mock<IContactService> _mockContactService;
    private readonly ContactUpdateConsumer _consumer;
    private readonly Fixture _fixture = new();
    private readonly InMemoryTestHarness _harness = new();
    private readonly Mock<ILogger<ContactUpdateConsumer>> _mockLogger = new();

    public ContactUpdateConsumerTests()
    {
        _mockContactService = new Mock<IContactService>();
        _consumer = new ContactUpdateConsumer(_mockLogger.Object, _mockContactService.Object);
    }
    
    [Fact]
    public async void ShouldProcessMessage_ContactUpdated_Success()
    {
        var contactResult = _fixture.Create<Contact>();
        var consumerHarness = _harness.Consumer(() => _consumer);
        _mockContactService.Setup(x =>
                x.UpdateAsync(It.IsAny<ContactUpdateEvent>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(contactResult);

        await _harness.Start();
        try
        {
            var message =  _fixture.Create<ContactUpdateEvent>();
            await _harness.InputQueueSendEndpoint.Send(message);
            (await _harness.Sent.Any<ContactUpdateEvent>()).Should().BeTrue();
            (await consumerHarness.Consumed.Any<ContactUpdateEvent>()).Should().BeTrue();
        }
        finally
        {
            await _harness.Stop();
        }
    }
}