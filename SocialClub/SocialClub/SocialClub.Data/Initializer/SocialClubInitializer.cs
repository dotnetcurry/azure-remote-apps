using SocialClub.Data.Context;
using SocialClub.Data.Entities;
using SocialClub.Data.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SocialClub.Data.Initializer
{
    public class SocialClubInitializer : CreateDatabaseIfNotExists<SocialClubDbContext>
    {
        protected override void Seed(SocialClubDbContext context)
        {
            var clubMembers = new List<ClubMember>{
                new ClubMember { Name = "Pete Darson", Occupation = (int)Occupation.Doctor, HealthStatus = (int)HealthStatus.Good, MaritalStatus = (int)MaritalStatus.Married, NumberOfChildren = 2, DateOfBirth = DateTime.Now.AddDays(-7500), Salary = 5500 },
                new ClubMember { Name = "Mat Pearson", Occupation = (int)Occupation.Engineer, HealthStatus = (int)HealthStatus.Excellent, MaritalStatus = (int)MaritalStatus.Single, NumberOfChildren = 0, DateOfBirth = DateTime.Now.AddDays(-6500), Salary = 3500 },
                new ClubMember { Name = "Dave Wood" , Occupation = (int)Occupation.Professor, HealthStatus = (int)HealthStatus.Good, MaritalStatus = (int)MaritalStatus.Single, NumberOfChildren = 0, DateOfBirth = DateTime.Now.AddDays(-6500), Salary = 3500},
                new ClubMember { Name = "Adam Nolan", Occupation = (int)Occupation.Engineer, HealthStatus = (int)HealthStatus.Excellent, MaritalStatus = (int)MaritalStatus.Single, NumberOfChildren = 0, DateOfBirth = DateTime.Now.AddDays(-6500), Salary = 3500}
            };

            clubMembers.ForEach(category => context.ClubMembers.Add(category));
        }
    }
}
