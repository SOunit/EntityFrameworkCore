using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;

namespace EntityFrameworkCore.ConsoleApp
{
    internal class Program
    {
        private static readonly FootballLeagueDbContext context = new FootballLeagueDbContext();

        static async Task Main(string[] args)
        {
            // approach1, table add
            //var league = new League { Name = "Red Stripe Premiere League" };
            //await context.Leagues.AddAsync(league);

            //await context.SaveChangesAsync();

            //await AddTeamsWithLeague(league);
            //await context.SaveChangesAsync();

            //approach2, vague add
            var league = new League { Name = "some league" };
            var team = new Team { Name = "some team", Leage = league };
            await context.AddAsync(team);
            await context.SaveChangesAsync();

            Console.WriteLine("Press Any Key To End...");
            Console.ReadKey();
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
    }
}