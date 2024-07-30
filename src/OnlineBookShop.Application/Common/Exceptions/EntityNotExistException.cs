namespace OnlineBookShop.Application.Common.Exceptions
{
    public class EntityNotExistException: Exception
    {
        public EntityNotExistException(string entity, int id): base($"Entity {entity} with id {id} does not exist")
        {

        }
    }
}
