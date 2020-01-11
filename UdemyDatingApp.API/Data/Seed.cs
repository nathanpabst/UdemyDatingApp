using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UdemyDatingApp.API.Models;

namespace UdemyDatingApp.API.Data
{
    public class Seed
    {
            // check db for users 
        public static void SeedUsers(DataContext context) 
        {
            // if no users exist in the db... 
            // add all user seed data from the json file to the userData var
            // convert the data into a list of user objects and store in the users var
            // loop through the users and: 
            // -generate a password hash and password salt property 
            // -convert the user name to lower case
            // -add the users to context
            // -save the changes
            // add seed method to the main method in Program.cs | best practice for seed data
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordSalt;
                    CreatePasswordHash("password", out passwordhash, out passwordSalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}