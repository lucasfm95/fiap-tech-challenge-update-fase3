using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.LibDomain.Events;
using MassTransit;
using Prometheus;

namespace Fiap.TechChallenge.Worker.Consumers;

public class ContactUpdateConsumer(ILogger<ContactUpdateConsumer> logger, IContactService contactService)
    : IConsumer<ContactUpdateEvent>
{
    private static readonly Counter ProcessedJobsCounter =
        Metrics.CreateCounter("updated_contact_total_processed", "Number of Contact Inserted consumed.");
    
    public async Task Consume(ConsumeContext<ContactUpdateEvent> context)
    {
        try
        {
            var result = await contactService.UpdateAsync(context.Message, context.CancellationToken);
            
            //Prometheus metrics
            ProcessedJobsCounter.Inc();
            
            logger.LogInformation("Update contact: {@contact}", result);
        }
        catch (Exception e)
        {
            logger.LogError("Error updating contact {@messageBody}: {Message}", context.Message, e.Message);
        }
    }
}