using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Service.Interfaces;
using INTRANET.Common;
using System.DirectoryServices;

namespace INTRANET.Service
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private static string _path = INTRANETConfiguration.LDAP;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                //check if it is not empty and it's a string
                _path = value;
            }
        }

        public dynamic GetUser(string userID, string password)
        {
            DirectoryEntry de = GetDirectoryEntry(userID, password);
            DirectorySearcher deSearch = new DirectorySearcher();

            deSearch.SearchRoot = de;
            deSearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userID + "))";
            deSearch.SearchScope = SearchScope.Subtree;

            try
            {
                SearchResult results = deSearch.FindOne();
                if (results != null)
                {
                    de = new DirectoryEntry(results.Path, userID, password, AuthenticationTypes.Secure);

                    string role = null;
                    if (PropertyExists(de.Properties, "memberOf"))
                    {
                        for (int i = 0; i < de.Properties["memberOf"].Count; i++)
                        {
                            string tmpRole = de.Properties["memberOf"][i].ToString().Split(',')[0].Split('=')[1].ToLower();
                            if (tmpRole.Contains("student"))
                            {
                                role = "student";
                                break;
                            }
                            else if (tmpRole.Contains("staff"))
                            {
                                role = "staff";
                                break;
                            }
                        }
                    }

                    return new
                    {
                        Username = (PropertyExists(de.Properties, "sAMAccountName")) ? de.Properties["sAMAccountName"].Value.ToString() : "",
                        Email = (PropertyExists(de.Properties, "mail")) ? de.Properties["mail"].Value.ToString() : "",
                        Role = role,
                        FirstName = (PropertyExists(de.Properties, "givenName")) ? de.Properties["givenName"].Value.ToString() : "",
                        LastName = (PropertyExists(de.Properties, "sn")) ? de.Properties["sn"].Value.ToString() : ""
                    };
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DirectoryEntry GetDirectoryEntry(string userID, string password)
        {
            DirectoryEntry de = new DirectoryEntry(_path, userID, password);
            de.AuthenticationType = AuthenticationTypes.Secure;
            return de;
        }

        public static bool PropertyExists(dynamic settings, string name)
        {
            try
            {
                var value = settings[name];
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
