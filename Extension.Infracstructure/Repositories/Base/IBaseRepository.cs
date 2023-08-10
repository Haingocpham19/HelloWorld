namespace Extension.Domain.Repositories
{
    public interface IBaseRepository<Entity>
    {
        Entity Insert(Entity entity);
        int Update(Entity entity);
        int Delete(Entity entity);
        Entity? GetById(object Id);
    }
}
