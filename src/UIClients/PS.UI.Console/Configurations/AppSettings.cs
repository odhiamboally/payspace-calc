using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Configurations;


internal sealed class AppSettings
{
    public string? ApiBaseUrl { get; set; }
    public int TimeoutSeconds { get; set; }
}
