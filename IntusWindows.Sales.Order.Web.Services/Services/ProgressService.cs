using IntusWindows.Sales.Order.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class ProgressService :IProgresService
{
    public EventHandler<long> ProgressChanged;
    public void Report(long value)
    {
        ProgressChanged?.Invoke(this, value);
        Console.WriteLine("updated progress");
    }
}
