using System;
using Stepometer.Models;
using Stepometer.Service.HttpApi.Repository;

namespace Stepometer.Service.HttpApi.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRestApiClient<StepometerModel> StepometerRestApiClient { get; }
    }
}
