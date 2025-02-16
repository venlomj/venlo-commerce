using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;