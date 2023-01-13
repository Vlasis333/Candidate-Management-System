using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Data.Repositories
{
    public class CertificateManagerRepository : ICertificateManagerRepository
    {
        private readonly AppDBContext _context;

        public CertificateManagerRepository()
        {
            _context = new AppDBContext();
        }

        public CertificateManagerRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all the certificates with thier information
        /// </summary>
        public async Task<IEnumerable<Certificate>> GetCertificates()
        {
            return await Task.Run(() => _context.Certificates.ToList());
        }
    }
}
