using Stepometer.Models;
using Stepometer.Service.HttpApi.Repository;
using System;
using System.Net.Http;

namespace Stepometer.Service.HttpApi.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private IRestApiClient<StepometerModel> _StepometerRestApiClient;

        public IRestApiClient<StepometerModel> StepometerRestApiClient =>
            _StepometerRestApiClient ??= new RestApiClient<StepometerModel>(_httpClient);


        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}