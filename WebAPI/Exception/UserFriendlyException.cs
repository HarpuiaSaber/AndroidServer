using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exception
{
    public class UserFriendlyException : CustomException
    {
        public UserFriendlyException(string message) : base(message)
        {

        }
    }
}
