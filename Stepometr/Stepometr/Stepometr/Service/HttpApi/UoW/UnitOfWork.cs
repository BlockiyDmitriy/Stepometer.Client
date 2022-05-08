using Stepometer.Models;
using Stepometer.Models.Login;
using Stepometer.Service.HttpApi.Repository;
using System;
using System.Net.Http;

namespace Stepometer.Service.HttpApi.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private IRestApiClient<StepometerModel> _StepometerRestApiClient;
        private IRestApiClient<RegisterModel> _LoginRestApiClient;

        public IRestApiClient<StepometerModel> StepometerRestApiClient =>
            _StepometerRestApiClient ??= new RestApiClient<StepometerModel>(_httpClient);
        public IRestApiClient<RegisterModel> LoginRestApiClient =>
            _LoginRestApiClient ??= new RestApiClient<RegisterModel>(_httpClient);

        public UnitOfWork()
        {
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
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