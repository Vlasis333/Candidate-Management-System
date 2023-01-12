using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _context;

        public AdminRepository()
        {
            _context = new AppDBContext();
        }

        public AdminRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method used to add new candidate to our DB
        /// </summary>
        public void AddCandidate(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            _context.CandidateLocations.Add(candidate.CandidateLocation);
            _context.CandidateContacts.Add(candidate.CandidateContact);
            _context.CandidatePhotoIdentifications.Add(candidate.CandidatePhotoIdentification);
            Save();
        }

        /// <summary>
        /// Method used to delete a candidate from the DB
        /// </summary>
        public void DeleteCandidate(int id)
        {
            var candidate = _context.Candidates.Include(c => c.CandidateCertificates).Where(p => p.CandidateId == id).SingleOrDefault();
            if (candidate != null)
            {

                DeleteCandidateCertificateEntites(candidate);

                try
                {
                    _context.CandidateContacts.Remove(candidate.CandidateContact);
                    _context.CandidateLocations.Remove(candidate.CandidateLocation);
                    _context.CandidatePhotoIdentifications.Remove(candidate.CandidatePhotoIdentification);
                    _context.Candidates.Remove(candidate);

                    Save();
                }
                catch (Exception ex)
                {
                    TextWriter errorWriter = Console.Error;
                    errorWriter.WriteLine(ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// Returns all the candidates with thier information
        /// </summary>
        public IEnumerable<Candidate> GetAllCandidates()
        {
            return _context.Candidates.ToList();
        }

        /// <summary>
        /// Returns a single candidate by given Id
        /// </summary>
        public Candidate GetCandidate(int id)
        {
            Candidate candidate = _context.Candidates
            .Include(c => c.CandidateLocation)
            .Include(c => c.CandidateContact)
            .Include(c => c.CandidatePhotoIdentification)
            .SingleOrDefault(c => c.CandidateId == id);
            return candidate;
        }

        /// <summary>
        /// Save changes from entity to the DB
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Update the selected candidate
        /// </summary>
        public void UpdateCandidate(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            _context.Entry(candidate.CandidateContact).State = EntityState.Modified;
            _context.Entry(candidate.CandidateLocation).State = EntityState.Modified;
            _context.Entry(candidate.CandidatePhotoIdentification).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Returns a list with all the certificates for every candidate
        /// </summary>
        public IEnumerable<Candidate> GetAllCandidatesWithCertificates()
        {
            return _context.Candidates.Include(c => c.CandidateCertificates).ToList();
        }

        /// <summary>
        /// Returns a list with every photo id type
        /// </summary>
        public IEnumerable<PhotoIdentificationType> GetAllPhotoIdentifications()
        {
            return _context.PhotoIdentificationTypes.ToList();
        }

        /// <summary>
        /// Deletes all the entites of the certificates for the current candidate (relationship entities)
        /// </summary>
        private void DeleteCandidateCertificateEntites(Candidate candidate)
        {
            try
            {
                var candidateCertificates = candidate.CandidateCertificates;

                foreach (CandidateCertificates candidateCertificate in candidateCertificates) // routine to delete all certificates and relationships of the candidate
                {
                    _context.Entry(candidateCertificate).Reference(p => p.CertificateAssessment).Load();
                    var assessmet = candidateCertificate.CertificateAssessment;

                    // we need to load the TopicAssessments as it is navigator on CandidateCertificates and we cant remove them just by reference on the navigator
                    // we need the object on memory block to remove all the items it contains (related to the candidate we delete)
                    var tempCandidateCertificates = _context.CandidateCertificates.Include(t => t.TopicAssesments).Where(p => p.CandidateCertificatesId == candidateCertificate.CandidateCertificatesId).SingleOrDefault();
                    var topicAssessments = tempCandidateCertificates.TopicAssesments;

                    _context.TopicAssesments.RemoveRange(topicAssessments);
                    _context.CertificateAssessments.Remove(assessmet);
                }

                // removes certificates obtained by candidate - relationship tables
                _context.CandidateCertificates.RemoveRange(candidateCertificates);
            }
            catch (Exception ex)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Dispose of the db context created locally
        /// </summary>
        private bool _dispose = false; // local bool that will ensure the dispose only executes when called
        public virtual void Dispose(bool disposing)
        {
            if (!this._dispose)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._dispose = true;
        }

        void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
