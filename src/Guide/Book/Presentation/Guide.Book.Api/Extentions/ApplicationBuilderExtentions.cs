using Guide.Book.Api.Consumers;

namespace Guide.Book.Api.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static EventBusReportCreateConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusReportCreateConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }
        public static void OnStarted()
        {
            Listener.Consume();
        }
        public static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
