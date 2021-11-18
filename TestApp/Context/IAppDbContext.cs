using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Context
{
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Purchase> Purchases { get; set; }
        DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChanges();
    }
}
