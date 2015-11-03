

namespace SocialClub.Data.Service
{
    using SocialClub.Data.Context;
    using SocialClub.Data.Entities;
    using SocialClub.Data.Extension;
    using SocialClub.Data.Service.Interface;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class ClubMemberService : IClubMemberService
    {
        
        /// <summary>
        /// Service method to get club member by Id
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>Data row</returns>
        public DataRow GetById(int id)
        {
            using (var context = new SocialClubDbContext())
            {
                ClubMember membership = context.ClubMembers
                    .Where(i => i.Id == id)
                    .SingleOrDefault();

                return membership.ToDataRow<ClubMember>();
            }            
        }

        /// <summary>
        /// Service method to get all club members
        /// </summary>
        /// <returns>Data table</returns>
        public DataTable GetAll()
        {
            using (var context = new SocialClubDbContext())
            {
                IList<ClubMember> members = context.ClubMembers.ToList();

                return members.ToDataTable<ClubMember>();
            }
        }

        /// <summary>
        /// Service method to search records by multiple parameters
        /// </summary>
        /// <param name="occupation">occupation value</param>
        /// <param name="maritalStatus">marital status</param>
        /// <param name="operand">AND OR operand</param>
        /// <returns>Data table</returns>
        public DataTable Search(int occupation, int maritalStatus, string operand)
        {
            using (var context = new SocialClubDbContext())
            {
                List<ClubMember> members = context.ClubMembers.Where(i => operand == "AND" ?
                    (occupation == 0 || occupation == i.Occupation) && 
                    (maritalStatus == 0 || maritalStatus == i.MaritalStatus) :
                    (occupation == 0 || occupation == i.Occupation) || 
                    (maritalStatus == 0 || maritalStatus == i.MaritalStatus))
                    .ToList();

                return members.ToDataTable<ClubMember>();
            }
        }        

        /// <summary>
        /// Service method to create new member
        /// </summary>
        /// <param name="clubMember">club member model</param>
        /// <returns>true or false</returns>
        public bool Create(ClubMember clubMember)
        {
            using (var context = new SocialClubDbContext())
            {
                context.ClubMembers.Add(clubMember);
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Service method to update club member
        /// </summary>
        /// <param name="clubMember">club member</param>
        /// <returns>true / false</returns>
        public bool Update(ClubMember clubMember)
        {
            using (var context = new SocialClubDbContext())
            {
                context.ClubMembers.Attach(clubMember);
                context.Entry(clubMember).State = EntityState.Modified;                
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Method to delete a club member
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>true / false</returns>
        public bool Delete(int id)
        {
            using (var context = new SocialClubDbContext())
            {
                var clubMember = context.ClubMembers.Find(id);
                context.ClubMembers.Remove(clubMember);
                return context.SaveChanges() > 0;
            }            
        }
    }
}
