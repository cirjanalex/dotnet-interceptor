using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericInterceptor
{
    public enum InterceptorMode
    {
        None,
        Recording,
        Replaying
    }

    public class InterceptorConfiguration
    {
        public InterceptorMode Mode { get; set; }
    }
}
