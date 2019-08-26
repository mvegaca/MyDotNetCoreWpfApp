using System.Collections.Generic;
using System.Windows;

namespace MyDotNetCoreWpfApp.Helpers
{
    public static class ApplicationExtensions
    {
        public static IEnumerable<string> GetProperties(this Application app)
        {
            foreach (var key in app.Properties.Keys)
            {
                yield return $"{key},{app.Properties[key]}";
            }
        }

        public static void SetProperties(this Application app, IEnumerable<string> propertyLines)
        {
            foreach (var line in propertyLines)
            {
                var settingPairValue = line.Split(',');
                app.Properties[settingPairValue[0]] = settingPairValue[1];
            }
        }
    }
}