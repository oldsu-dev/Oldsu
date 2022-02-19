﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Types;

namespace Oldsu.DatabaseServices.MySql
{
    public class MySqlUserService : DbContext, IUserService
    {
        private DbSet<AuthenticationPair> AuthenticationPairs { get; set; }
        private DbSet<UserInfo> UserInfo { get; set; }

        public async Task<UserInfo?> AuthenticateAsync(string username, string passwordHash)
        {
            var authenticationPair = await AuthenticationPairs
                .Where(auth => auth.User.Username == username).Include(auth => auth.User)
                .FirstOrDefaultAsync();

            if (authenticationPair == null)
                return null;
            
            if (BCrypt.Net.BCrypt.Verify(passwordHash, authenticationPair.Password))
                return authenticationPair.User;
            
            return null;
        }

        public async Task<UserInfo?> GetUserInfo(uint userId)
        {
            return await UserInfo.FindAsync(userId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }

    }
}