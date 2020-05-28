using System;
using System.Collections.Generic;
using System.Web;

namespace MyDotNetCoreWpfApp.Models
{
    public class SchemeActivationData
    {
        // TODO WTS: Open package.appxmanifest and change the declaration for the scheme (from the default of 'wtsapp') to what you want for your app.
        // More details about this functionality can be found at https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/UWP/features/deep-linking.md
        // TODO WTS: Change the image in Assets/Logo.png to one for display if the OS asks the user which app to launch.
        // Also update this protocol name with the same value as package.appxmanifest.
        public const string ProtocolName = "wtswpfapp";

        public string ViewModelName { get; private set; }

        public Uri Uri { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        public bool IsValid => !string.IsNullOrEmpty(ViewModelName);

        public SchemeActivationData(Uri activationUri)
        {
            ViewModelName = SchemeActivationConfig.GetViewModelName(activationUri.AbsolutePath);

            if (!IsValid || string.IsNullOrEmpty(activationUri.Query))
            {
                return;
            }

            var uriQuery = HttpUtility.ParseQueryString(activationUri.Query);
            foreach (var paramKey in uriQuery.AllKeys)
            {
                Parameters.Add(paramKey, uriQuery.Get(paramKey));
            }
        }

        public SchemeActivationData(string viewModelName, Dictionary<string, string> parameters = null)
        {
            ViewModelName = viewModelName;
            Parameters = parameters;
            Uri = BuildUri();
        }

        private Uri BuildUri()
        {
            var pageKey = SchemeActivationConfig.GetPageKey(ViewModelName);
            var uriBuilder = new UriBuilder($"{ProtocolName}:{pageKey}");
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var parameter in Parameters)
            {
                query.Set(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = query.ToString();
            return new Uri(uriBuilder.ToString());
        }
    }
}
