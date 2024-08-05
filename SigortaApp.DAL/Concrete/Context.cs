using SigortaApp.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.DAL.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        //public Context() : base() { } //Bu satır ve onconfiguring fonksiyonu açılacak.
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=SigortaAppSanayiDb;User Id=SA;Password=Password6506.;TrustServerCertificate=True;Encrypt=false;");
        ////    //optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Server =localhost;Port=5432;Database=SigortaAppDb;");
        ////    //optionsBuilder.UseSqlServer(@"Data Source=CBS-C26TKJY\SQLEXPRESS;Initial Catalog=SigortaAppProjectDb;Integrated Security=true;Pooling=False;");//Bakanlık
        ////    //optionsBuilder.UseSqlServer(@"Data Source=DONANIMTKP06S2;Initial Catalog=SigortaAppProjectDb;Integrated Security=true;Pooling=False;");//Bakanlık UZAKTAN
        ////    optionsBuilder.UseSqlServer("Server=172.17.26.183;Database=SigortaAppProjectDb;User Id=bydonanim;Password=DonanimTakip06.;");
        ////    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-5QTA1UV\SQLEXPRESS;Initial Catalog=SigortaAppProjectDb;Integrated Security=true;Pooling=False;");//EV
        ////    //optionsBuilder.UseSqlServer(@"workstation id=SigortaAppDb.mssql.somee.com;packet size=4096;user id=custom;pwd=123456789.;data source=SigortaAppDb.mssql.somee.com;persist security info=False;initial catalog=SigortaAppDb;Integrated Security=true;Pooling=False;");
        ////    //optionsBuilder.UseNpgsql("User ID=root;Password=1234;Host=localhost;Port=5432;Database=SigortaAppDb;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message2>()
                .HasOne(x => x.SenderUser)
                .WithMany(y => y.Sender)
                .HasForeignKey(z => z.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(y => y.Receiver)
                .HasForeignKey(z => z.ReveiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<BankAndCaseDetails>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<BlogRayting> BlogRaytings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message2> Message2s { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<DevicesRelease> DevicesRelease { get; set; }
        public DbSet<Unit> Unit { get; set; }

        //Sanayi için yazılanlar

        public DbSet<Task> Task { get; set; }
        public DbSet<TaskHistory> TaskHistory { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<PaymentTask> PaymentTask { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FileTask> FileTask { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<UnitTypes> UnitTypes { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<BankAndCaseDetails> BankAndCaseDetails { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
    }
}
