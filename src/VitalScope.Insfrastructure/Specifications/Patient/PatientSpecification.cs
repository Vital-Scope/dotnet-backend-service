using Microsoft.EntityFrameworkCore;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Specifications.Patient;

public class PatientSpecification : BaseSpecification<PatientEntity>
{
    public PatientSpecification(Guid id, bool trackable = false) : base(x=>x.Id == id && !x.IsDeleted)
    {
        Trackable = trackable;
        
        AddInclude(x=>x.Include(b=>b.StudyMetaInformations)
            .ThenInclude(q=>q.InfoMetas));
    }
    
    public PatientSpecification(bool trackable = false) : base(DefaultSpecification.EmptyCriteria<PatientEntity>())
    {
        Trackable = trackable;
    }
}