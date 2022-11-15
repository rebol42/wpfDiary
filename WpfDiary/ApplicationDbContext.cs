namespace WpfDiary
{
    using DiaryWPF.Models.Configurations;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using WpfDiary.Models;
    using WpfDiary.Models.Domains;
    using WpfDiary.Properties;
   

    public class ApplicationDbContext : DbContext
    {

        // pierwsza praca domowa 
        private static string _server = Settings.Default.Server;
        private static string _serverDbName = Settings.Default.ServerDbName;
        private static string _database = Settings.Default.Database;
        private static string _user =  Settings.Default.User;
        private static string _password = Settings.Default.Password;

        private static string _dbConnectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};"
            , _server, _database, _user, _password);


        public ApplicationDbContext()
            : base(_dbConnectionString)
        {
        }

        //DbSet - klasa generyczna , która na podstawie klas Domains bedzie wiedziala jakie ma utworzyc tabele
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
        }


        public void dbConnection(DbConnect dbConnect)
        {

        }
        

    }


}