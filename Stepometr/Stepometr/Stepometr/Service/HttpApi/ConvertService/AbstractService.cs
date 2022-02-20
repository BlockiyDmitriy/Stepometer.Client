using Stepometer.Service.HttpApi.UoW;
using Xamarin.Forms;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public abstract class AbstractService
    {
        protected IUnitOfWork UOW { get; set; }

        private bool _disposed = false;
        protected AbstractService(IUnitOfWork uOW)
        {
            this.UOW = uOW;
        }
        protected AbstractService()
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
