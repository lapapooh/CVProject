using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Common
{
    public static class INTRANETConfiguration
    {
        public static string LDAP
        {
            get
            {
                return ConfigurationHelper.GetSetting("LDAP");
            }
        }
        public static string LDAPUsername
        {
            get
            {
                return ConfigurationHelper.GetSetting("LDAPUsername");
            }
        }
        public static string LDAPPassword
        {
            get
            {
                return ConfigurationHelper.GetSetting("LDAPPassword");
            }
        }

        public static string UploadsUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("UploadsUrl");
            }
        }
        public static string LectureAttachmentsPath
        {
            get
            {
                return ConfigurationHelper.GetSetting("LectureAttachmentsPath");
            }
        }
        public static string TutorialAttachmentsPath
        {
            get
            {
                return ConfigurationHelper.GetSetting("TutorialAttachmentsPath");
            }
        }
    }
}
