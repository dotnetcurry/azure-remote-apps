using SocialClub.Data.Entities;
using SocialClub.Data.Initializer;
using System.Data.Entity;

namespace SocialClub.Data.Context
{
    public class SocialClubDbContext : DbContext
    {
        public SocialClubDbContext()
            : base("SocialClub.DbConnection")
        {
            Database.SetInitializer<SocialClubDbContext>(new SocialClubInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ClubMember> ClubMembers { get; set; }
    }
}
