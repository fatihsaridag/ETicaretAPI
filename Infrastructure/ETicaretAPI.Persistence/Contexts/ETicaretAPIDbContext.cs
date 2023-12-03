using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            //Bundan sonra her saveChanges tetiklendiğinde ben insert ve update yapılan tüm dataları elde edilip üzerine istediğimiz değişikliği yapıp ardından saveChangesAsync'i devreye sokabilirim.


            //ChangeTracker : Entityler üzerinden yapılan degisikliklerin yada yeni eklenen verilerin yakalanmasını sağlayan propertylerdir Update operasoyonlarında Track edilen verieri yakalayıp elde etmemizi sağlar.

           var datas =  ChangeTracker
                .Entries<EntityBase>();             //Buradaki gelen dataları yakaladık

            foreach (var data in datas)
            {
                 _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }


        //Biz herhangi bir veriyi eklerken veya güncellerken save fonksiyonunu cagırıp SaveChangesAsync Tetiklersek burada ilk önce override tetiklenecek. Gelen dataları yakalıcaz ve yakalanan datalarda state'i added veya modifed olanları CreatedDate ve UpdatedDate güncellicez. Ve saveChanges'ı bir daha cagırıyoruz . Ordadaki degisikliklere göre sql sorgularını olusturacak. 

    }
}
