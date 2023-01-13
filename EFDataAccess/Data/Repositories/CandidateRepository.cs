using EFDataAccess.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iTextSharp.text.pdf.parser;
using Org.BouncyCastle.Crypto.Tls;

namespace EFDataAccess.Data.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDBContext _context;

        public CandidateRepository()
        {
            _context = new AppDBContext();
        }

        public CandidateRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return information for the current candidate
        /// </summary>
        public async Task<Candidate> GetCandidate(int candidateId)
        {
            return await Task.Run(() => _context.Candidates.Find(candidateId));
        }

        /// <summary>
        /// Return the certificates of a certain candidate
        /// </summary>
        public async Task<IEnumerable<CandidateCertificates>> GetCertificatesOfCandidate(int candidateId)
        {
            var currentCandidate = _context.Candidates.Include("CandidateCertificates").Where(p => p.CandidateId == candidateId).SingleOrDefault();
            var candidateCertificates = currentCandidate.CandidateCertificates;

            foreach (CandidateCertificates candidateCertificate in candidateCertificates)
            {
                LoadCandidateCertificates(_context, candidateCertificate);
            }

            return await Task.Run(() => candidateCertificates.ToList());
        }

        /// <summary>
        /// Download a pdf file with all the certificates of a candidate and returns a string with the info needed of the method (save or no save)
        /// </summary>
        public async Task<string> DownloadAllCertificatesOfCandidate(int candidateId)
        {
            // Export of Candidate’s Certificates in a .pdf format
            var currentCandidate = _context.Candidates.Include("CandidateCertificates").Where(p => p.CandidateId == candidateId).SingleOrDefault();
            var candidateCertificates = currentCandidate.CandidateCertificates;
            string returnString = "";

            try
            {
                // routine to load pdf string and print it to the pdf page,
                // then save the file to a location
                var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                using (var memoryStream = new MemoryStream()) // start new memory stream to write data on the pdf and save it to hard drive
                {
                    var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    iTextSharp.text.Font fontDarkBlue = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DarkBlue)); // set font style to be used later

                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(new Chunk($"Certificates of {currentCandidate.FirstName} {currentCandidate.LastName} (all attempts)", fontDarkBlue)); // Title of the PDF
                    pdfDoc.Add(new Paragraph(" "));

                    foreach (CandidateCertificates candidateCertificate in candidateCertificates)
                    {
                        _context.Entry(candidateCertificate).Reference(p => p.CertificateAssessment).Load();
                        CertificateAssessment assessmet = candidateCertificate.CertificateAssessment;

                        _context.Entry(candidateCertificate).Reference(p => p.Certificate).Load();
                        Models.Certificate certificate = candidateCertificate.Certificate;

                        pdfDoc.Add(new Paragraph(""));

                        pdfDoc.Add(new Chunk("CERTIFICATE: ", fontDarkBlue)); // chuck = same line formating
                        pdfDoc.Add(new Chunk(certificate.Title));
                        pdfDoc.Add(new Paragraph("")); // paragraph = Enviroment.NewLine

                        pdfDoc.Add(new Chunk("Assessment Test Code: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(certificate.AssessmentTestCode + " "));
                        pdfDoc.Add(new Chunk("Examination Date: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(candidateCertificate.ExaminationDate.ToShortDateString()));
                        pdfDoc.Add(new Paragraph(""));

                        pdfDoc.Add(new Chunk("Score Report Date: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(assessmet.ScoreReportDate.ToShortDateString() + " "));
                        pdfDoc.Add(new Chunk("Candidate: Score: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(assessmet.CandidateScore.ToString()));
                        pdfDoc.Add(new Paragraph(""));

                        pdfDoc.Add(new Chunk("Maximum Score: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(assessmet.MaximumScore.ToString() + " "));
                        pdfDoc.Add(new Chunk("Percentage: Score: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(assessmet.PercentageScore.ToString() + " %"));
                        pdfDoc.Add(new Paragraph(""));

                        pdfDoc.Add(new Chunk("Assessment Result: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(assessmet.AssessmentResult.ToString() + " "));
                        pdfDoc.Add(new Chunk("Active: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(certificate.Active.ToString()));

                        pdfDoc.Add(new Paragraph(" "));
                    }

                    pdfDoc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    string fileName = $@"\{currentCandidate.FirstName} {currentCandidate.LastName} Certificates.pdf";
                    File.WriteAllBytes(path + fileName, bytes);

                    returnString = path + fileName;

                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return await Task.Run(() => returnString);
        }

        /// <summary>
        /// Download a pdf file with a single certificates and it's topics
        /// </summary>
        public async Task<string> DownloadSelectedCertificateOfCandidate(int candidateId, int certificateId)
        {
            var currentCandidate = _context.Candidates.Include("CandidateCertificates").Where(p => p.CandidateId == candidateId).SingleOrDefault();
            var candidateCertificate = currentCandidate.CandidateCertificates.Where(c => c.Certificate.CertificateId == certificateId).SingleOrDefault();
            string returnString = "";

            try
            {
                var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                using (var memoryStream = new MemoryStream())
                {
                    var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    iTextSharp.text.Font fontDarkBlue = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DarkBlue));

                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(new Chunk($"Certificate {candidateCertificate.Certificate.Title}", fontDarkBlue)); 
                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(new Chunk($"Candidate {currentCandidate.FirstName} {currentCandidate.LastName}", fontDarkBlue));
                    pdfDoc.Add(new Paragraph(" "));

                    _context.Entry(candidateCertificate).Reference(p => p.CertificateAssessment).Load();
                    var assessmet = candidateCertificate.CertificateAssessment;                                      

                    pdfDoc.Add(new Paragraph(""));

                    pdfDoc.Add(new Chunk("Certificate Info", fontDarkBlue)); 
                    pdfDoc.Add(new Paragraph(""));

                    pdfDoc.Add(new Chunk("Assessment Test Code: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(candidateCertificate.Certificate.AssessmentTestCode + " "));
                    pdfDoc.Add(new Chunk("Examination Date: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(candidateCertificate.ExaminationDate.ToShortDateString()));
                    pdfDoc.Add(new Paragraph(""));

                    pdfDoc.Add(new Chunk("Score Report Date: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(assessmet.ScoreReportDate.ToShortDateString() + " "));
                    pdfDoc.Add(new Chunk("Candidate: Score: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(assessmet.CandidateScore.ToString()));
                    pdfDoc.Add(new Paragraph(""));

                    pdfDoc.Add(new Chunk("Maximum Score: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(assessmet.MaximumScore.ToString() + " "));
                    pdfDoc.Add(new Chunk("Percentage: Score: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(assessmet.PercentageScore.ToString() + " %"));
                    pdfDoc.Add(new Paragraph(""));

                    pdfDoc.Add(new Chunk("Assessment Result: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(assessmet.AssessmentResult.ToString() + " "));
                    pdfDoc.Add(new Chunk("Active: ", fontDarkBlue));
                    pdfDoc.Add(new Chunk(candidateCertificate.Certificate.Active.ToString()));

                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Chunk("Topics Info", fontDarkBlue));
                    pdfDoc.Add(new Paragraph(""));

                    var topicAssesments = candidateCertificate.TopicAssesments;

                    foreach (TopicAssesment assesment in topicAssesments)
                    {
                        _context.Entry(assesment).Reference(p => p.Topic).Load();
                        var topic = assesment.Topic;

                        pdfDoc.Add(new Chunk("Topic Title: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk(topic.Title.ToString() + " "));
                        pdfDoc.Add(new Chunk("Topic Result: ", fontDarkBlue));
                        pdfDoc.Add(new Chunk($"{assesment.AwardedMarks} / {topic.PossibleMarks}"));

                        pdfDoc.Add(new Paragraph(""));
                    }

                    pdfDoc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    string fileName = $@"\{currentCandidate.FirstName} {currentCandidate.LastName} Certificate {candidateCertificate.Certificate.Title}.pdf";
                    File.WriteAllBytes(path + fileName, bytes);

                    returnString = path + fileName;

                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return await Task.Run(() => returnString);
        }

        /// <summary>
        /// Method used to load all connections of the candidate certificate to other tables (using navigators)
        /// </summary>
        private static void LoadCandidateCertificates(AppDBContext appDBContext, CandidateCertificates candidateCertificate)
        {
            appDBContext.Entry(candidateCertificate).Reference(p => p.CertificateAssessment).Load();
            CertificateAssessment assessmet = candidateCertificate.CertificateAssessment;

            appDBContext.Entry(candidateCertificate).Reference(p => p.Certificate).Load();
            Models.Certificate certificate = candidateCertificate.Certificate;
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
