using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Doc.Core.Documentation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins
{
    public static class MDILoader{
        public static async Task LoadMdi(HttpClient client, NavigationManager navigator)
        {
            try
            {

                byte[] licenseResult = await client.GetByteArrayAsync($"{navigator.BaseUri}/_content/OpenCodeDev.Blazor.Foundation.Doc.Core/mdi_database.txt");
                string MDIRawDB = Encoding.UTF8.GetString(licenseResult);
                string pattern = @"(?<=\}\.)(.*?)(?=\:)";
                Regex rg = new Regex(pattern);
                MatchCollection matches = rg.Matches(MDIRawDB);
                Mdi.MdiDatabase = matches.Select(p => p.Value).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

        }


    }


}
