using BizTalkComponents.Utilities.LookupUtility.Repository;
using Microsoft.BizTalk.SSOClient.Interop;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.SSOLookupUtility
{
    public class SSOLookupRepository : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list, TimeSpan maxAge = default(TimeSpan))
        {
            var propertyBag = SSOClientHelper.BuildPropertyBag(list);
            var dictionary = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> entry in propertyBag.Properties)
            {
                dictionary.Add(entry.Key, entry.Value);
            }

            return dictionary;
        }

        internal class ConfigurationPropertyBag : IPropertyBag
        {
            public HybridDictionary Properties { get; set; }
            internal ConfigurationPropertyBag()
            {
                Properties = new HybridDictionary();
            }
            public void Read(string propName, out object ptrVar, int errLog)
            {
                ptrVar = Properties[propName];
            }
            public void Write(string propName, ref object ptrVar)
            {
                Properties.Add(propName, ptrVar);
            }
            public bool Contains(string key)
            {
                return Properties.Contains(key);
            }
            public void Remove(string key)
            {
                Properties.Remove(key);
            }
        }
        internal static class SSOClientHelper
        {
            private static string idenifierGUID = "ConfigProperties";
            internal static ConfigurationPropertyBag BuildPropertyBag(string appName)
            {
                try
                {
                    SSOConfigStore ssoStore = new SSOConfigStore();
                    ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();
                    ((ISSOConfigStore)ssoStore).GetConfigInfo(appName, idenifierGUID, SSOFlag.SSO_FLAG_RUNTIME, (IPropertyBag)appMgmtBag);
                    return appMgmtBag;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message);
                    throw;
                }
            }
            /// <summary>
            /// Read method helps get configuration data
            /// </summary>        
            /// <param name="appName">The name of the affiliate application to represent the configuration container to access</param>
            /// <param name="propName">The property name to read</param>
            /// <returns>
            ///  The value of the property stored in the given affiliate application of this component.
            /// </returns>
            private static string Read(string appName, string propName)
            {
                try
                {
                    SSOConfigStore ssoStore = new SSOConfigStore();
                    ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();
                    ((ISSOConfigStore)ssoStore).GetConfigInfo(appName, idenifierGUID, SSOFlag.SSO_FLAG_RUNTIME, (IPropertyBag)appMgmtBag);
                    object propertyValue = null;
                    appMgmtBag.Read(propName, out propertyValue, 0);
                    return (string)propertyValue;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
