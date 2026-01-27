using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEvidenceFileRepository
    {
        Task<EvidenceFile> GetEvidenceFileById(Guid id);
        Task<IEnumerable<EvidenceFile>> GetEvidenceFilesByAppointmentId(Guid id);
        Task<IEnumerable<EvidenceFile>> GetEvidenceFileByPatient(Guid id);
        Task CreateEvidenceFile(EvidenceFile evidenceFile);
        Task<EvidenceFile> UpdateEvidenceFile(EvidenceFile evidenceFile);
        Task DeleteEvidenceFile(Guid id);
    }
}