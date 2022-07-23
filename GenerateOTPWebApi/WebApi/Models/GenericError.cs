namespace WebApi.Models;

/// <summary>
/// Generic error structure
/// </summary>
public class GenericError
{
    /// <summary>
    /// Initializes a new instance of <see cref="GenericError"/>
    /// </summary>
    /// <param name="message">Message to display</param>
    public GenericError(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Gets the message for the error
    /// </summary>
    public string Message { get; }
}
