using LiteDB;
using Stepometer.Models;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.LoaclDB
{
    public class DBService : IDBService
    {
        private readonly LiteDatabase _liteDatabase;
        private readonly ILiteCollection<StepometerModel> _stepometerModel;
        private readonly ILiteCollection<AvgHistoryWebModel> _avgHistoryCollection;
        private readonly ILiteCollection<ActivityDate> _activityDateCollection;

        private ILogService _logService { get; set; }

        public DBService()
        {
            _liteDatabase = new LiteDatabase(DBHelper.DBPath);
            _stepometerModel = _liteDatabase.GetCollection<StepometerModel>(DBHelper.StepometerCollection);
            _avgHistoryCollection = _liteDatabase.GetCollection<AvgHistoryWebModel>(DBHelper.AvgHistoryCollection);
            _activityDateCollection = _liteDatabase.GetCollection<ActivityDate>(DBHelper.ActivityDateId);

            _logService = DependencyResolver.Get<ILogService>();
        }

        public Task<StepometerModel> SetStepometerDataAsync(StepometerModel stepometerModel)
        {
            try
            {
                _stepometerModel.Insert(stepometerModel);
                return Task.FromResult(stepometerModel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Task.FromResult(stepometerModel ?? new StepometerModel());
            }
        }

        public Task<StepometerModel> GetStepometerDataAsync()
        {
            try
            {
                var data = _stepometerModel.FindAll().LastOrDefault();
                return Task.FromResult(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Task.FromResult(new StepometerModel());
            }
        }

        public Task<StepometerModel> UpdateStepometerDataAsync(StepometerModel stepometerModel)
        {
            try
            {
                var result = _stepometerModel.Update(stepometerModel);
                if (!result)
                {
                    _logService.Log("Local db. Document not found");
                    _stepometerModel.Insert(stepometerModel);
                }
                return Task.FromResult(stepometerModel);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return Task.FromResult(stepometerModel ?? new StepometerModel());
            }
        }

        public Task<AvgHistoryWebModel> SetHistoryDataAsync(AvgHistoryWebModel avgHistoryModel)
        {
            try
            {
                _avgHistoryCollection.Insert(avgHistoryModel);
                return Task.FromResult(avgHistoryModel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Task.FromResult(avgHistoryModel ?? new AvgHistoryWebModel());
            }
        }

        public Task<AvgHistoryWebModel> GetHistoryDataAsync()
        {
            try
            {
                var data = _avgHistoryCollection.FindAll().LastOrDefault();
                return Task.FromResult(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Task.FromResult(new AvgHistoryWebModel());
            }
        }

        public Task<AvgHistoryWebModel> UpdateHistoryDataAsync(AvgHistoryWebModel avgHistoryModel)
        {
            try
            {
                var result = _avgHistoryCollection.Update(avgHistoryModel);
                if (!result)
                {
                    _logService.Log("Local db. Document not found");
                    _avgHistoryCollection.Insert(avgHistoryModel);
                }
                return Task.FromResult(avgHistoryModel);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return Task.FromResult(avgHistoryModel ?? new AvgHistoryWebModel());
            }
        }

        public Task UpdateLastActivityDate(DateTimeOffset date)
        {
            try
            {
                var activityDate = _activityDateCollection.FindOne(a => a.Id == DBHelper.ActivityDateId);

                if (activityDate == null)
                {
                    activityDate = new ActivityDate
                    {
                        Id = DBHelper.ActivityDateId,
                        Date = date
                    };
                    _activityDateCollection.Insert(activityDate);

                    return Task.CompletedTask;
                }
                else
                {
                    activityDate.Date = date;
                    _activityDateCollection.Update(activityDate);

                    return Task.CompletedTask;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Task.FromResult(new DateTimeOffset());
            }
        }

        public Task<DateTimeOffset> GetLastActivityDate()
        {
            var activityDate = _activityDateCollection.FindOne(a => a.Id == DBHelper.ActivityDateId);
            return Task.FromResult(activityDate == null ? default(DateTimeOffset) : activityDate.Date);
        }
    }
    public class ActivityDate
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
