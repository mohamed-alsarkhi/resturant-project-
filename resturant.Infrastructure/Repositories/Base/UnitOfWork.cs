using resturant.Infrastructure.Data;

namespace resturant.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            CategoryRepo = new CategoryRepository(_db);
            MenuItemRepo = new MenuItemRepository(_db);
            PermissionRepo = new PermissionRepository(_db);
            RoleRepo = new RoleRepository(_db);
            RoleUserRepo = new RoleUserRepository(_db);
            UserFileRepo = new UserFileRepository(_db);
            UserRepo = new UserRepository(_db);
            OrderRepo = new OrderRepository(_db);
            CartRepo = new CartRepository(_db);
        }

        public ICategoryRepository CategoryRepo { get; }
        public IMenuItemRepository MenuItemRepo { get; }
        public IPermissionRepository PermissionRepo { get; }
        public IRoleRepository RoleRepo { get; }
        public IRoleUserRepository RoleUserRepo { get; }
        public IUserFileRepository UserFileRepo { get; }
        public IUserRepository UserRepo { get; }

        public IOrderRepository OrderRepo { get; }
        public ICartRepository CartRepo { get; }

        

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}