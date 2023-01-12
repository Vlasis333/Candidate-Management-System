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
        IEnumerable<Candidate> GetAllCandidates();
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Candidate GetCandidate(int id);
        void AddCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        void DeleteCandidate(int id);
        void Save();
        PhotoIdentificationType GetPhotoIdentificationType(int id);
        IEnumerable<Candidate> GetAllCandidatesWithCertificates();
        Task<IEnumerable<Candidate>> GetAllCandidatesWithCertificatesAsync();
        IEnumerable<PhotoIdentificationType> GetAllPhotoIdentifications();
    }
}
