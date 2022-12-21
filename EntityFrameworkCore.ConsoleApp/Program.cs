using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

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

            //Simple Select Queries
            //SimpleSelectQuery();

            //Query with filters
            //await QueryFilters();

            //Aggregate Functions
            //await AdditionalExecutionMethods();

            //Altenative Linq Syntax
            //await AlternativeLinqSyntax();

            //perform update
            //await SimpleUpdateLeagueRecord();
            //await SimpleUpdateTeamRecord();

            //perform delete
            //await SimpleDelete();
            //await DeleteWithRelationship();

            // tracking vs. no tracking
            await TrackingVsNoTacking();

            Console.WriteLine("Press Any Key To End...");
            Console.ReadKey();
        }

        private static async Task TrackingVsNoTacking()
        {
            var withTacking = await context.Teams.FirstOrDefaultAsync(q => q.Id == 2);
            var withNoTacking = await context.Teams.AsNoTracking().FirstOrDefaultAsync(q => q.Id == 8);

            withTacking.Name = "Update1";
            withNoTacking.Name = "Update2";

            var entriesBeforeSave = context.ChangeTracker.Entries();

            await context.SaveChangesAsync();
            
            var entriesAfterSave = context.ChangeTracker.Entries();
        }

        private static async Task DeleteWithRelationship()
        {
            var league = await context.Leagues.FindAsync(9);
            context.Leagues.Remove(league);
            await context.SaveChangesAsync();
        }

        private static async Task SimpleDelete()
        {
            // cascade means delete all relation record
            // when league is deleted, related teams are also deleted

            // restrict is for stopping delete

            // setNull is set null for foreign key
            var league = await context.Leagues.FindAsync(4);
            context.Leagues.Remove(league);
            await context.SaveChangesAsync();
        }

        private async static Task SimpleUpdateTeamRecord()
        {
            var team = new Team
            {
                Id = 1,
                Name = "Update",
                LeageId = 2
            };

            // 1. this can be add without offering Id
            // 2. update with Id
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }

        private static async Task SimpleUpdateLeagueRecord()
        {
            // Retrieve Record
            var league = await context.Leagues.FindAsync(3);
            // Make Record Changes
            league.Name = "Update Name";
            // Save Changes
            await context.SaveChangesAsync();

            await GetRecord();
        }

        private static async Task GetRecord()
        {
            var league = await context.Leagues.FindAsync(3);
            Console.WriteLine($"{league.Id} - ${league.Name}");
        }

        private static async Task AlternativeLinqSyntax()
        {
            Console.WriteLine("Enter Team Name (Or Part Of): ");
            var teamName = Console.ReadLine();
            var teams = await (from i in context.Teams
                               where EF.Functions.Like(i.Name, $"%{teamName}%")
                               select i).ToListAsync();
            foreach (var team in teams)
            {
                Console.WriteLine($"{team.Id} - {team.Name}");
            }
        }

        private static async Task AdditionalExecutionMethods()
        {
            //var l = context.Leagues.Where(q => q.Name.Contains("A")).FirstOrDefaultAsync();
            //var l = context.Leagues.FirstOrDefaultAsync(q => q.Name.Contains("A"));

            var leagues = context.Leagues;
            var list = await leagues.ToListAsync();
            
            // expect returns list and get first. if not list, error
            var first = await leagues.FirstAsync();
            var firstOrDefault = await leagues.FirstOrDefaultAsync();

            // execpt 1 record, if not error.
            //var single = await leagues.SingleAsync();
            //var singleOrDefault = await leagues.SingleOrDefaultAsync();

            var count = await leagues.CountAsync();
            var longCount = await leagues.LongCountAsync();
            
            // error because it's not mathematics
            var min = await leagues.MinAsync();
            var max = await leagues.MaxAsync();
        }

        private static async Task QueryFilters()
        {
            Console.Write("League Name: ");
            var leagueName = Console.ReadLine();
            var leagues = await context.Leagues.Where(q => q.Name.Equals(leagueName)).ToListAsync();
            foreach (var league in leagues)
            {
                Console.WriteLine($"{league.Id} - {league.Name}");
            }

            //var partialMatches = await context.Leagues.Where(q => q.Name.Contains(leagueName)).ToListAsync();
            var partialMatches = await context.Leagues
                .Where(q => EF.Functions.Like(q.Name, $"%{leagueName}%"))
                .ToListAsync();
            foreach (var league in partialMatches)
            {
                Console.WriteLine($"{league.Id} - {league.Name}");
            }
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