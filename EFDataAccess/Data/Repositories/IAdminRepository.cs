using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Data.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Candidate>> GetAllCandidates();
        Task<Candidate> GetCandidate(int id);
        Task AddCandidate(Candidate candidate);
        Task UpdateCandidate(Candidate candidate);
        Task DeleteCandidate(int id);
        Task Save();
        Task<PhotoIdentificationType> GetPhotoIdentificationType(int id);
        Task<IEnumerable<Candidate>> GetAllCandidatesWithCertificates();
        Task<IEnumerable<PhotoIdentificationType>> GetAllPhotoIdentifications();
    }
}
