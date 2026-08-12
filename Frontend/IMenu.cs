using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend
{
    internal interface IMenu
    {
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
