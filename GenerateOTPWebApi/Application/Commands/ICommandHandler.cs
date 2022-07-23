namespace Application.Commands;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> ProcessCommandAsync(TCommand command);
}
