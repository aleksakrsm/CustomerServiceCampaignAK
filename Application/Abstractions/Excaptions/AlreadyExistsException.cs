namespace Application.Abstractions.Excaptions
{
    public class AlreadyExistsException : EveralApplicationException
    {
        public AlreadyExistsException(string entityName, Guid id)
            : base($"{entityName} with id {id} already exists")
        {
        }
    }
}
