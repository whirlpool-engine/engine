﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Whirlpool.Core.Pattern;

namespace Whirlpool.Game.Logic
{
    public class User
    {
        public string username;
    }

    public class UserAPI : Singleton<UserAPI>
    {
        HttpClient client = new HttpClient();
        private User currentUser;
        public User loggedInUser { get { return currentUser; } }

        public static bool LogIn(string username, string password)
        {
            var instance = GetInstance();
            instance.currentUser = new User();
            var values = new Dictionary<string, string>
                {
                   { "username", username },
                   { "password", password },
                   { "type", "login" }
                };
            var content = new FormUrlEncodedContent(values);
            var response = instance.client.PostAsync("http://gu3.me/oslo/api/user.php", content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            if (responseString.Result == "success")
            {
                instance.currentUser.username = username;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}