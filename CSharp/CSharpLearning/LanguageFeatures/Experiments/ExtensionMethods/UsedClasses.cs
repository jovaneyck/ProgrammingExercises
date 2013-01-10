using System;

namespace LanguageFeatures.Experiments.ExtensionMethods
{
    static class ExtensionDefiner
    {
        public static String MyExtension(this ExtensionUtilizer utilizer)
        {
            return "Hello, I am an extension! Native: " + utilizer.MyProperty();
        }
    }

    class ExtensionUtilizer
    {
        public String MyProperty()
        {
        get:
            {
                return "NATIVE!";
            }
        }
    }
}
