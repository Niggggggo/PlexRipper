﻿using Infrastructure.UnitTests.API;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlexRipper.Domain.ValueObjects;
using PlexRipper.Infrastructure.Services;
using System.IO;

namespace Infrastructure.UnitTests.Services
{
    public class BaseServiceTest
    {
        public BaseServiceTest()
        {

        }


        public static CredentialsDTO GetCredentials()
        {
            // Create a secretCredentials.json in the root of Infrastructure.UnitTests project and add Plex testing credentials.
            // {
            //    "username": "SomeUsername",
            //    "password": "Password123"
            // }
            using (StreamReader r = new StreamReader("secretCredentials.json"))
            {
                string json = r.ReadToEnd();
                JObject o = JObject.Parse(json);
                if (o.ContainsKey("username") && o.ContainsKey("password"))
                {
                    return JsonConvert.DeserializeObject<CredentialsDTO>(json);
                }

            }
            BaseDependanciesTest
                .GetLogger<BaseServiceTest>()
                .LogWarning("MAKE SURE TO CREATE A \"secretCredentials.json\" IN THE Infrastructure.UnitTests project TO START TESTING!");
            return new CredentialsDTO();
        }

        public static PlexService GetPlexService()
        {
            return new PlexService(
                BaseDependanciesTest.GetDbContext(),
                BaseDependanciesTest.GetMapper(),
                BaseApiTest.GetPlexApi(),
                BaseDependanciesTest.GetLogger<PlexService>());
        }

        public static AccountService GetAccountService()
        {
            return new AccountService(
                BaseDependanciesTest.GetDbContext(),
                BaseDependanciesTest.GetMapper(),
                GetPlexService(),
                BaseDependanciesTest.GetLogger<AccountService>());
        }
    }
}
