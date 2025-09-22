using Microsoft.EntityFrameworkCore;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Specifications.Study;

public class StudySpecification : BaseSpecification<StudyMetaInformationEntity>
{
    public StudySpecification(bool trackable = false) : base(DefaultSpecification.EmptyCriteria<StudyMetaInformationEntity>())
    {
        Trackable = trackable;
        
        AddInclude(t => t.Include(b => b.InfoMetas));

    }
    
    public StudySpecification(Guid id, bool trackable = false) : base(x=>x.Id == id)
    {
        Trackable = trackable;
        
        AddInclude(t => t.Include(b => b.InfoMetas));
    }
}