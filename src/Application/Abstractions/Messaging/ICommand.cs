using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging;


//internal interface ICommand : IRequest<Result>, IBaseCommand
//{

//}

internal interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand
{}

internal interface IBaseCommand;
