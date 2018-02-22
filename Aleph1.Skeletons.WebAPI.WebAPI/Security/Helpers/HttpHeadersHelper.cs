using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security.Helpers
{
    internal static class HttpHeadersHelper
    {
        internal static string GetAuthenticationInfoValue(this HttpRequestHeaders headers)
        {
            return headers.FirstOrDefault(h => h.Key.Equals(SettingsManager.AuthenticationHeaderKey, StringComparison.OrdinalIgnoreCase)).Value?.FirstOrDefault();
        }

        internal static void AddAuthenticationInfoValue(this HttpHeaders headers, string value)
        {
            if (headers.Contains(SettingsManager.AuthenticationHeaderKey))
                headers.Remove(SettingsManager.AuthenticationHeaderKey);

            if (!String.IsNullOrWhiteSpace(value))
                headers.Add(SettingsManager.AuthenticationHeaderKey, value);
        }

        internal static T GetHttpParameter<T>(this HttpAuthenticationContext context, params string[] parameterNames)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(context.Request.RequestUri.Query);

            string possibleValue = null;
            foreach (string parameterName in parameterNames)
            {
                possibleValue = query[parameterName]
                   ?? context?.ActionContext?.ControllerContext?.RouteData?.Values[parameterName]?.ToString();

                if (!String.IsNullOrWhiteSpace(possibleValue))
                    break;
            }

            if (!String.IsNullOrWhiteSpace(possibleValue))
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)typeConverter.ConvertFromString(possibleValue);
            }

            throw new ArgumentNullException(String.Join(",", parameterNames));
        }
    }
}