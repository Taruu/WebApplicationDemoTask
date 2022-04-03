using Microsoft.EntityFrameworkCore;

namespace WebApplicationGuide.Models
{
    //Иницилизация бд и всего такого
    public class ElectricityСountContext : DbContext
    {
        public ElectricityСountContext(DbContextOptions<ElectricityСountContext> options)
            : base(options)
        {
        }

        public DbSet<ElectricityCount> ElectricityCount { get; set; } //Референс на таблицу 0 _ 0
        public DbSet<ElectricityValue> ElectricityValues { get; set; }

        //After Table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Уникальный серийны номер для счетчика
            //Делает индекс уникального значения... Почему нельзя исползовать напрямую UNIQUE? Потому что Да
            modelBuilder.Entity<ElectricityCount>().HasIndex(item => item.SerialNumber).IsUnique();

            //Cвязи наши мысли сильный
            modelBuilder.Entity<ElectricityValue>().HasOne(DataItem => DataItem.ElectricityCount)
                .WithMany().HasForeignKey(DataItem => DataItem.ElectricityCountForeignKey);
        }
    }
}