using Microsoft.EntityFrameworkCore;
using VitalScope.Common.Enums;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Specifications.Study;

public class StudySpecification : BaseSpecification<StudyMetaInformationEntity>
{
    public StudySpecification(bool trackable = false) : base(DefaultSpecification.EmptyCriteria<StudyMetaInformationEntity>())
    {
        Trackable = trackable;
        
        AddInclude(t => t.Include(b => b.InfoMetas));
        
        AddInclude(r=>r.Include(x=>x.Patient));

    }
    
    public StudySpecification(Guid id, bool trackable = false) : base(x=>x.Id == id)
    {
        Trackable = trackable;
        
        AddInclude(t => t.Include(b => b.Patient));
        
        AddInclude(t => t.Include(b => b.InfoMetas));
    }
    
    public StudySpecification(StatusType statusType, bool trackable = false) : base(x=>x.Status == statusType)
    {
        Trackable = trackable;
        
        AddInclude(t => t.Include(b => b.Patient));
    }
    
    public StudySpecification(StatusType statusType) : base(x=>x.Status == statusType)
    {
        Trackable = false;
        
        AddInclude(t => t.Include(b => b.Patient));
    }
}