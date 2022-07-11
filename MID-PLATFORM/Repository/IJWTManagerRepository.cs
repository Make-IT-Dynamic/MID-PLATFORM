using MID_PLATFORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MID_PLATFORM.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(User user);
    }
}
