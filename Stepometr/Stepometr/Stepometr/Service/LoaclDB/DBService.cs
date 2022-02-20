using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Stepometer.Models;

namespace Stepometer.Service.LoaclDB
{
    public class DBService : IDBService
    {
        private readonly LiteDatabase _liteDatabase;
        private readonly ILiteCollection<StepometerModel> _stepometerModel;
        private readonly ILiteCollection<ActivityDate> _activityDateCollection;

        public DBService()
        {
            _liteDatabase = new LiteDatabase(DBHelper.DBPath);
            _stepometerModel = _liteDatabase.GetCollection<StepometerModel>(DBHelper.StepometerCollection);
            _activityDateCollection = _liteDatabase.GetCollection<ActivityDate>(DBHelper.ActivityDateId);
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
                throw;
            }
        }

        public Task<StepometerModel> GetStepometerDataAsync()
        {
            try
            {
                var data = _stepometerModel.FindAll().FirstOrDefault();
                return Task.FromResult(data == null ? new StepometerModel() : data);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public Task<StepometerModel> UpdateStepometerDataAsync(StepometerModel stepometerModel)
        {
            try
            {
                if (_stepometerModel.Update(stepometerModel))
                {
                    _stepometerModel.Insert(stepometerModel);
                }
                return Task.FromResult(stepometerModel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
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
                throw;
            }
        }

        public DateTimeOffset GetLastActivityDate()
        {
            var activityDate = _activityDateCollection.FindOne(a => a.Id == DBHelper.ActivityDateId);
            return activityDate == null ? default(DateTimeOffset) : activityDate.Date;
        }
    }
    public class ActivityDate
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
