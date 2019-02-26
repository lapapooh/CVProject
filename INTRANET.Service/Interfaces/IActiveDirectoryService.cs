using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Service.Interfaces
{
    public interface IActiveDirectoryService
    {
        dynamic GetUser(string userID, string password);
        //string GetUserFullName(string userID, string password);
        //string GetUserEmail(string userID, string password);
    }
}
