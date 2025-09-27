using System.Text;
using MQTTnet;
using MQTTnet.Protocol;
using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Logic.Services;

public sealed class MQTTService : IMQTTService
{
    private readonly IStudyService _studyService;

    public MQTTService(IStudyService studyService)
    {
        _studyService = studyService;
    }

    public async Task SubscribeAsync(SensorModel model, CancellationToken cancellationToken = default)
    {
      //  await _studyService.AddMain(new MainSensorInputModel())
    }
}