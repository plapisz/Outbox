using MediatR;

namespace Outbox.Samples.Api.Commands;

public sealed record CreateOrder(Guid Id, string Number, string CustomerEmail) : IRequest;
