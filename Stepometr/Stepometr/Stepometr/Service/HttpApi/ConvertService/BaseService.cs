using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;
using Xamarin.Forms;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public abstract class BaseService
    {
        protected ILogService _logService { get; set; }
        protected string _baseUrl { get; set; } = Constants.Constants.BaseUrl;
        protected IUnitOfWork UOW { get; set; }

        private bool _disposed = false;
        protected BaseService(IUnitOfWork uOW)
        {
            this.UOW = uOW;
            _logService = DependencyResolver.Get<ILogService>();
        }
        protected BaseService()
        {
            UOW ??= DependencyService.Get<IUnitOfWork>();
            _logService = DependencyResolver.Get<ILogService>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UOW.Dispose();
                }
            }
            _disposed = true;
        }

    }
}
