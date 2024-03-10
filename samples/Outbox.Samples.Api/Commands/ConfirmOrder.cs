using MediatR;

namespace Outbox.Samples.Api.Commands;

public sealed record ConfirmOrder(Guid Id) : IRequest;
