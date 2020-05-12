using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exception
{
    public class CustomException : System.Exception
    {
        public int Code { get; set; }
        public CustomException(string message) : base(message)
        {
            Code = 500;
        }
    }
}
