using VitalScope.Common.Helpers;
using VitalScope.Insfrastructure.Models;
using VitalScope.Logic.Models.Output.MainSensor;
using VitalScope.Logic.Models.Output.MetaSensor;

namespace VitalScope.Logic.Mappings;

public static class MonitoringMappings
{
    public static MetaSensorOutputModel EntityToOutputModel(this StudyMetaInformationEntity entity)
    {
        return new MetaSensorOutputModel
        {
            Id = entity.Id,
            MedicalTests = entity.ValidateFields()
                ? new MedicalTestOutputModel()
                {
                    Lac = entity.Lac,
                    Be = entity.Be,
                    Ph = entity.Ph,
                    Glu = entity.Glu,
                    СarbonDioxide = entity.СarbonDioxide,
                }
                : null,
            Status = entity.Status,
            Result = entity.Result,
            DateStart = entity.DateStart.ToTime(),
            DateEnd = entity.DateEnd.ToTime(),
            Diagnosis = entity.Diagnosis,
            PatientId = entity.PatientId,
            Sensors = entity.InfoMetas.Select(x => new MainSensorOutputModel
            {
                Id = x.Id,
                Time = x.Time,
                Channel = x.Channel,
                Value = x.Value
            }),
            CreatedAt = entity.CreatedAt.ToTime(),
            UpdatedAt = entity.UpdatedAt.ToTime(),
            Notes = entity.Notes,
            FullName = GetFullName(entity.Patient)
        };
    }

    public static string GetFullName(PatientEntity? patient)
    {
        if (patient == null)
        {
            return string.Empty;
        }

        var result = string.Empty;

        if (!string.IsNullOrWhiteSpace(patient.LastName))
        {
            result += patient.LastName + " ";
        }

        if (!string.IsNullOrWhiteSpace(patient.FirstName))
        {
            result += patient.FirstName + " ";
        }

        if (!string.IsNullOrWhiteSpace(patient.MiddleName))
        {
            result += patient.MiddleName + " ";
        }

        return result.Trim();
    }
}