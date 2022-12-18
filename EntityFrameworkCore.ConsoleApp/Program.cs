using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;

namespace EntityFrameworkCore.ConsoleApp
{
    internal class Program
    {
        private static readonly FootballLeageDbContext context = new FootballLeageDbContext();

        static void Main(string[] args)
        {
            context.Leagues.Add(new League { Name = "Red Stripe Premiere League" });
            context.SaveChanges();
        }
    }
}