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
        Candidate GetCandidate(int candidateId);
        IEnumerable<CandidateCertificates> GetCertificatesByCandidateId(int candidateId);
        string DownloadAllCertificatesOfCandidate(int candidateId);
    }
}
