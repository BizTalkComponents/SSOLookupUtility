using BizTalkComponents.Utilities.LookupUtility.Application;
using System;
using BizTalkComponents.Utilities.LookupUtility;
using System.Diagnostics;

namespace BizTalkComponents.Utilities.SSOLookupUtility
{
    public class SSOApplicationService : IApplicationService
    {
        private LookupUtilityService _lookupUtilityService;
        public SSOApplicationService()
        {
            _lookupUtilityService = new LookupUtilityService(new SSOLookupRepository());

        }

        public string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false)
        {
            string value;

            try
            {
                value = _lookupUtilityService.GetValue(list, key, throwIfNotExists, allowDefaults);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("An exception was thrown in LookupUtility {0}", ex.ToString()));
                throw ex;
            }

            return value;
        }

        public string GetValue(string list, string key, string defaultValue)
        {
            string value;

            try
            {
                value = _lookupUtilityService.GetValue(list, key, defaultValue);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("An exception was thrown in LookupUtility {0}", ex.ToString()));
                throw ex;
            }

            return value;
        }
    }
}
