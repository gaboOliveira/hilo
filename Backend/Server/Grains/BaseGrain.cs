using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Server.Grains
{
    public class BaseGrain<T> : Grain where T : IGrain
    {
        protected readonly ILogger<T> logger;

        public BaseGrain(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetRequiredService<ILogger<T>>();
        }

        public override Task OnActivateAsync()
        {
            if (this is IGrainWithIntegerKey)
                logger.LogInformation("{grainTypeName} activated for {id}", this.GetType().Name, this.GetPrimaryKeyLong());
            else if (this is IGrainWithGuidKey)
                logger.LogInformation("{grainTypeName} activated for {id}", this.GetType().Name, this.GetPrimaryKey());
            if (this is IGrainWithStringKey)
                logger.LogInformation("{grainTypeName} activated for {id}", this.GetType().Name, this.GetPrimaryKeyString());

            return base.OnActivateAsync();
        }
    }
}
