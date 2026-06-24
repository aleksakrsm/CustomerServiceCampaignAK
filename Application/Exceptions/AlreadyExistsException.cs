namespace Application.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string entityName, Guid id)
            : base($"{entityName} with id {id} already exists")
        {
        }
    }
}
