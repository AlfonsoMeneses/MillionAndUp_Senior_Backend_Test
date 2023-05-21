using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Exceptions
{
    public  class PropertyException: Exception
    {
        public PropertyException() { }

        public PropertyException(string message) : base(message) { }
    }
}
