using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;

namespace EntityFrameworkCore.ConsoleApp
{
    internal class Program
    {
        private static readonly FootballLeagueDbContext context = new FootballLeagueDbContext();

        static async Task Main(string[] args)
        {
            //Simple Insert Operation Methods
            //await AddNewLeague();
            //await AddNewTeamsWithLeague();

            SimpleSelectQuery();

            Console.WriteLine("Press Any Key To End...");
            Console.ReadKey();
        }

        private static void SimpleSelectQuery()
        {
            //smartest most efficient way to get results
            var leagues = context.Leagues.ToList();
            foreach (var league in leagues)
            {
                Console.WriteLine($"{league.Id} - {league.Name}");
            }

            //inefficient way to get results. keep connection open until completed
            //and might create lock on table
            //foreach (var league in context.Leagues)
            //{
            //    Console.WriteLine($"{league.Id} - {league.Name}");
            //}
        }

        private static async Task AddNewLeague()
        {
            //Adding a new league object
            var league = new League { Name = "Red Stripe Premiere League" };
            await context.Leagues.AddAsync(league);
            await context.SaveChangesAsync();

            //function to add new teams related to the new league object
            await AddTeamsWithLeague(league);
            await context.SaveChangesAsync();
        }

        private static async Task AddTeamsWithLeague(League league)
        {
            var teams = new List<Team>
            {
                new Team { Name = "name1", LeageId = league.Id },
                new Team { Name = "name2", LeageId = league.Id },
                new Team { Name = "name3", Leage = league },
            };

            await context.AddRangeAsync(teams);
        }

        private static async Task AddNewTeamsWithLeague()
        {
            var league = new League { Name = "New League" };
            var team = new Team { Name = "New Team", Leage = league };
            await context.AddAsync(team);
            await context.SaveChangesAsync();
        }
    }
}