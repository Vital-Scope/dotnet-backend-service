using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Models.Input.MetaSensor;
using VitalScope.Logic.Models.Output.MainSensor;
using VitalScope.Logic.Models.Output.MetaSensor;

namespace VitalScope.Logic.Services.Study;

public interface IStudyService
{
    Task<MetaSensorOutputModel> AddMetaAsync(MetaSensorInputModel model, CancellationToken cancellationToken = default);
    
    Task<MetaSensorOutputModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<MetaSensorOutputModel>> GetAllMetaWithSensorAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<MetaSensorWithoutSensorOutputModel>> GetAllMetaAsync(CancellationToken cancellationToken = default);

    
    Task AddMain(MainSensorInputModel  model, CancellationToken cancellationToken = default);
    
    Task AddMainItem(SensorModel model, IEnumerable<SensorModel> models, CancellationToken cancellationToken = default);
    
    Task AddMainItems(IEnumerable<SensorModel> models, CancellationToken cancellationToken = default);

    
    Task<IEnumerable<MainSensorOutputModel>> GetByIdMainAsync(Guid metaId, CancellationToken cancellationToken = default);
    
    Task<MetaSensorOutputModel> EditMetaAsync(EditMetaSensorInputModel model , CancellationToken cancellationToken = default);
}