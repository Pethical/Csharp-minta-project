using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pethical.Framework.Utils
{
    public static class ServiceFinder
    {
        public static TService GetInstance<TService>()
        {
            try
            {
                return ServiceLocator.Current.GetInstance<TService>();
            }
            catch (ActivationException exception)
            {
                if (typeof(TService) != typeof(ILogger))
                    ServiceFinder.GetInstance<ILogger>().Warn(exception.Message);
                return default(TService);
            }
        }
    }


}
