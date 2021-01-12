using System;

using Microsoft.Extensions.Logging;

using gsm;

namespace gsm.MVVM.ViewModels
{
    public abstract class ViewModel : ObservableObject
    {
        protected readonly ILogger Logger;

        protected ViewModel(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory?.CreateLogger(this.GetType()) ?? throw new ArgumentNullException(nameof(loggerFactory));

            Logger.LogInformation($"{this.GetType().ToString()} instantiated.");
        }
    }
}