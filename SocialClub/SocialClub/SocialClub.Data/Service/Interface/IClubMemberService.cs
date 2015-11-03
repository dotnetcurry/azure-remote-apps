using SocialClub.Data.Entities;
using System.Collections.Generic;
using System.Data;

namespace SocialClub.Data.Service.Interface
{
    public interface IClubMemberService
    {
        /// <summary>
        /// Method to get all club members
        /// </summary>
        /// <returns>Data table</returns>
        DataRow GetById(int Id);

        /// <summary>
        /// Service method to get all club members
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAll();

        /// <summary>
        /// Service method to search records by multiple parameters
        /// </summary>
        /// <param name="occupation">occupation value</param>
        /// <param name="maritalStatus">marital status</param>
        /// <param name="operand">AND OR operand</param>
        /// <returns>Data table</returns>
        DataTable Search(int occupation, int maritalStatus, string operand);

        /// <summary>
        /// Service method to create new member
        /// </summary>
        /// <param name="clubMember">club member model</param>
        /// <returns>true or false</returns>
        bool Create(ClubMember clubMember);

        /// <summary>
        /// Method to update club member details
        /// </summary>
        /// <param name="clubMember">club member</param>
        /// <returns></returns>
        bool Update(ClubMember clubMember);

        /// <summary>
        /// Method to delete a club member
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>true / false</returns>
        bool Delete(int id);       
    }
}
