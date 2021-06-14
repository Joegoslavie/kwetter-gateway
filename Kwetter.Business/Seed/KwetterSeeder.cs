namespace Kwetter.Business.Seed
{
    using Kwetter.Business.Manager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Faker;
    using Kwetter.DataAccess.Model;

    /// <summary>
    /// Class for generating Kwetter data to work with or for demo purposes.
    /// This is essentially also a stress / integration test since it relies on actively running microservices
    /// and thus will also trigger Kafka and other events.
    /// </summary>
    public class KwetterSeeder
    {
        private readonly AuthenticationManager authenticationManager;
        private readonly AccountManager accountManager;
        private readonly FollowManager followManager;
        private readonly ProfileManager profileManager;
        private readonly TweetManager tweetManager;

        private readonly string defaultPassword = "Password";

        public KwetterSeeder(AuthenticationManager authManager, AccountManager accountManager, FollowManager followManager, ProfileManager profileManager, TweetManager tweetManager)
        {
            this.authenticationManager = authManager;
            this.accountManager = accountManager;
            this.followManager = followManager;
            this.profileManager = profileManager;
            this.tweetManager = tweetManager;
        }

        public async Task RunAll(int userAmount)
        {
             var accounts = await this.SeedUsers(userAmount).ConfigureAwait(false);
            // wait a few seconds for Kafka just to be sure.
            await Task.Delay(TimeSpan.FromSeconds(3));

            accounts = await this.SeedUpdateProfile(accounts).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromSeconds(3));

            await this.SeedTweets(accounts, 3).ConfigureAwait(false);
            await this.SeedFollowers(accounts).ConfigureAwait(false);

            Console.WriteLine($"Seeded {userAmount} Kwetter users");
        }

        public async Task SeedFollowers(List<Account> accounts)
        {
            var tasks = new List<Task>();
            var tempAccounts = new List<Account>(accounts);

            foreach (var account in accounts)
            {
                var currentId = account.Id;
                var ids = tempAccounts.OrderBy(x => x.Username).Take(accounts.Count / 2).Where(x => x.Id != currentId).Select(a => a.Id).ToList();
                tasks.AddRange(ids.Select(x => this.followManager.ToggleFollow(currentId, x)));
            }

            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while seeding followers {ex.Message}");
            }
        }


        /// <summary>
        /// Creates new user accounts in Kwetter, for each user the profile and  
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Account>> SeedUsers(int amount)
        {
            List<Task<Account>> newUsers = new List<Task<Account>>();
            for (int i = 0; i < amount; i++)
            {
                var username = Internet.UserName();
                var email = Internet.Email();
                newUsers.Add(this.authenticationManager.Register(username, defaultPassword, email));
            }

            try
            {
                await Task.WhenAll(newUsers).ConfigureAwait(false);
                return newUsers.Select(x => x.Result).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while seeding users {ex.Message}");
                return new List<Account>();
            }
        }

        public async Task<List<Account>> SeedUpdateProfile(List<Account> accounts)
        {
            List<Task<Profile>> updatedProfiles = new List<Task<Profile>>();
            for (int i = 0; i < accounts.Count; i++)
            {
                var acc = accounts[i];
                updatedProfiles.Add(this.profileManager.Update(acc.Id, acc.Username, Name.FullName(), Internet.Url(), Lorem.Paragraph(20), Country.Name()));
            }

            try
            {
                await Task.WhenAll(updatedProfiles).ConfigureAwait(false);
                accounts.ForEach(x => x.Profile = updatedProfiles.FirstOrDefault(y => y.Result.UserId == x.Id).Result);
                return accounts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while updating profiles {ex.Message}");
                return accounts;
            }
        }

        public async Task SeedTweets(List<Account> accounts, int tweetsPerUser)
        {
            var random = new Random();
            var tasks = new List<Task>();

            try
            {
                foreach (var acc in accounts)
                {
                    for (int i = 0; i < tweetsPerUser; i++)
                    {
                        if (random.Next(1, 15) == 6)
                        {
                            var randomUser = accounts.Where(x => x.Id != acc.Id).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            tasks.Add(this.tweetManager.Place(1, $"Hey there @{randomUser?.Username} what are you doing??"));
                        }
                        else
                        {
                            tasks.Add(this.tweetManager.Place(acc.Id, $"Some random tweet..."));
                        }
                    }
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            //for (int i = 0; i < accounts.Count; i++)
            //{
            //    for (int y = 0; y < tweetsPerUser; y++)
            //    {
            //        tasks.Add(this.tweetManager.Place(accounts[i].Id, Lorem.Sentence(random.Next(5, 25))));
            //    }
            //}

            //try
            //{
            //    await Task.WhenAll(tasks);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception occured while seeding tweets {ex.Message}");
            //}
        }

    }
}
