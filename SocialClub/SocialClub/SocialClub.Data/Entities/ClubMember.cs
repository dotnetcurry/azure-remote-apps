using System;
using System.ComponentModel;

namespace SocialClub.Data.Entities
{
    public class ClubMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public int Occupation { get; set; }

        public Nullable<decimal> Salary { get; set; }

        public int MaritalStatus { get; set; }

        public int HealthStatus { get; set; }

        public Nullable<int> NumberOfChildren { get; set; }
    }
}
