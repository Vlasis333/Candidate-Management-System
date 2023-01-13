using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Data.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidate(int candidateId);
        Task<IEnumerable<CandidateCertificates>> GetCertificatesByCandidateId(int candidateId);
        Task<string> DownloadAllCertificatesOfCandidate(int candidateId);
    }
}
