using Stepometer.Service.HttpApi.UoW;
using Xamarin.Forms;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public abstract class BaseService
    {
        protected string _baseUrl { get; set; } = Constants.Constants.BaseUrl;
        protected IUnitOfWork UOW { get; set; }

        private bool _disposed = false;
        protected BaseService(IUnitOfWork uOW)
        {
            this.UOW = uOW;
        }
        protected BaseService()
        {
            UOW ??= DependencyService.Get<IUnitOfWork>();
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
