using System.IO;
using System.Threading.Channels;
using GraphEosStreamer.AssemblyGenerator;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GraphEosStreamer.BlockReader;
using GraphEosStreamer.GraphQl.Schemas.Block;
using GraphEosStreamer.SHiP.Variants;
using GraphQL;
using Microsoft.Extensions.Options;

namespace GraphEosStreamer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            if(!Directory.Exists(RuntimeAssemblyGenerator.AssemblyPath))
                Directory.CreateDirectory(RuntimeAssemblyGenerator.AssemblyPath);
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .Configure<Options>(options => Configuration.GetSection("Options").Bind(options))
                .AddSingleton<BlockReaderService>()
                .AddHostedService<BlockReaderService>(provider => provider.GetService<BlockReaderService>())
                .AddHostedService<BlockDeserializerService>()
                .AddHostedService<DeltaDeserializerService>()
                .AddSingleton(Channel.CreateUnbounded<byte[]>(new UnboundedChannelOptions() { SingleReader = false, SingleWriter = true}))
                .AddSingleton(Channel.CreateUnbounded<GetBlocksResultV0>(new UnboundedChannelOptions() { SingleReader = false, SingleWriter = false}))
                .AddSingleton(svc => svc.GetRequiredService<Channel<byte[]>>().Reader)
                .AddSingleton(svc => svc.GetRequiredService<Channel<byte[]>>().Writer)
                .AddSingleton(svc => svc.GetRequiredService<Channel<GetBlocksResultV0>>().Reader)
                .AddSingleton(svc => svc.GetRequiredService<Channel<GetBlocksResultV0>>().Writer)
                .AddSingleton<IBlockStream, BlockStream>()
                .AddSingleton<BlockStreamSchema>()
                .AddSingleton<IDocumentExecuter>(sp => new SubscriptionDocumentExecuter())
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = Environment.IsDevelopment();
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                // Add required services for GraphQL request/response de/serialization
                .AddSystemTextJson() // For .NET Core 3+
//                .AddNewtonsoftJson() // For everything else
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                .AddWebSockets() // Add required services for web socket support
                .AddDataLoader() // Add required services for DataLoader support
                .AddGraphTypes(typeof(BlockStreamSchema)); // Add all IGraphType implementors in assembly which ChatSchema exists 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // this is required for websockets support
            app.UseWebSockets();

            // use websocket middleware for ChatSchema at default path /graphql
            app.UseGraphQLWebSockets<BlockStreamSchema>();

            // use HTTP middleware for ChatSchema at default path /graphql
            app.UseGraphQL<BlockStreamSchema>();

            // use GraphiQL middleware at default path /ui/graphiql with default options
            app.UseGraphQLGraphiQL();

            // use GraphQL Playground middleware at default path /ui/playground with default options
            app.UseGraphQLPlayground();

            // use Altair middleware at default path /ui/altair with default options
            app.UseGraphQLAltair();

            // use Voyager middleware at default path /ui/voyager with default options
            app.UseGraphQLVoyager();

            app.UseRouting();
        }
    }
}
