namespace resturant.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IMenuItemRepository MenuItemRepo { get; }
        IPermissionRepository PermissionRepo { get; }
        IRoleRepository RoleRepo { get; }
        IRoleUserRepository RoleUserRepo { get; }
        IUserFileRepository UserFileRepo { get; }
        IUserRepository UserRepo { get; }
        IOrderRepository OrderRepo { get; }
        ICartRepository CartRepo { get; }

        void Save();
    }
}
