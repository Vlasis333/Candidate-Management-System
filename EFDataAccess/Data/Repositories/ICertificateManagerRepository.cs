using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Data.Repositories
{
    public interface ICertificateManagerRepository
    {
        Task<IEnumerable<Certificate>> GetCertificates();
        Task<Certificate> GetCertificate(int id);
    }
}
