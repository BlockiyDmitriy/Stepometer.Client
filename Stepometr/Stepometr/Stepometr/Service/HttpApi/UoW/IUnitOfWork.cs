using System;
using Stepometer.Models;
using Stepometer.Models.Login;
using Stepometer.Service.HttpApi.Repository;

namespace Stepometer.Service.HttpApi.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRestApiClient<StepometerModel> StepometerRestApiClient { get; }
        IRestApiClient<RegisterModel> LoginRestApiClient { get; }
        IRestApiClient<HistoryUserParamWebModel> HistoryRestApiClient { get; }
    }
}
