using Sinq.DTO;
using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Converters
{
    public class GenericConverter
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var converterMethod = typeof(GenericConverter).GetMethods()
                .FirstOrDefault(m =>
                {
                    if (m.ReturnParameter.ParameterType != typeof(TDestination))
                        return false;

                    var parameters = m.GetParameters();
                    if (parameters.Length == 1 &&
                        parameters[0].ParameterType == typeof(TSource))
                        return true;
                    return false;
                });
            if (converterMethod != null)
            {
                var result = converterMethod.Invoke(new GenericConverter(), new object[] { source });
                return (TDestination)result;
            }
            return default(TDestination);
        }
        
        public ActivityDTO Activity_FromModelToDTO(Activity model)
        {
            return new ActivityDTO()
            {
                Id = model.Id,
                Completed = model.Completed,
                Name = model.Name,
                ElapsedTime = model.ActivityTimes
                    .Sum(activityTime =>
                        ((activityTime.EndDate ?? DateTimeOffset.Now) -
                        activityTime.StartDate).TotalSeconds),
                DueDate = model.DueDate,
                IsStarted = model.IsStarted()
            };
        }

    }
}