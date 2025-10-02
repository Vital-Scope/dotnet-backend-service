using VitalScope.Logic.Models.Input.Patient;
using VitalScope.Logic.Models.Output.Patient;

namespace VitalScope.Logic.Patient;

public interface IPatientService
{
    Task<PatientResultModel> AddAsync(PatientModel patient, CancellationToken cancellationToken = default);
    
    Task<PatientResultModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<PatientResultModel>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<PatientResultModel> UpdateAsync(EditPatientModel patient, CancellationToken cancellationToken = default);
}