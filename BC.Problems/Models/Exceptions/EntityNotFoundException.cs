namespace BC.Problems.Models.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, Guid providedId)
        : base($"{entityName} with provided id: {providedId} cannot be found!")
    {

    }

    public EntityNotFoundException()
        : base("Entity was not found!")
    {

    }
}
