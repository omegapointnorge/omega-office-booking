﻿using server.Models.Domain;

namespace server.Models.DTOs.Internal
{
    public class User
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public User(String name, String id)
        {
            UserName = id;
            UserId = name;
        }
    }
}
