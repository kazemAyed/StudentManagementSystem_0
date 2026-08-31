using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0
{
    public class clsUtility
    {

        public static bool Again()
        {
            Console.Write("Again? [Y,N]: ");
            return string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsValidUrl(string url)
        {
            return
                Uri.TryCreate
                (
                    url,
                    UriKind.Absolute,
                    out var uriResult
                )
                &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        }

    }

}
