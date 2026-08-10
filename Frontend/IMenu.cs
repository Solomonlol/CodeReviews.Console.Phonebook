using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend
{
    internal interface IMenu
    {
        //Dictionary<string, Func<Task>> Menu { get; }

        //Task Create(CancellationToken cancellationToken = default);
        //Task Update(CancellationToken cancellationToken = default);
        //Task Delete(CancellationToken cancellationToken = default);

        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
