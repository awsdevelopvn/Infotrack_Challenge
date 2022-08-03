using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application;

public class CommandBase<T> where T: IEntity
{
    protected T PrepareNewEntity(T entity, User requestUser)
    {
        // entity.CreatedBy = requestUser;
        // entity.IsActive = true;
        // entity.CreatedOn = DateTime.UtcNow;

        return entity;
    }
    
    protected T PrepareUpdateEntity(T entity, User requestUser)
    {
        // entity.UpdatedBy = requestUser;
        // entity.UpdatedOn = DateTime.UtcNow;

        return entity;
    }
}