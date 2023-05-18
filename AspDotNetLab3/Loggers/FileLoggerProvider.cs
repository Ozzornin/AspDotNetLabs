namespace AspDotNetLab3.Loggers
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        public FileLoggerProvider(IConfiguration config, IHttpContextAccessor accessor)
        {
            _configuration = config;
            _accessor = accessor;
        }

        public void Dispose() { }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_configuration, _accessor);
        }
    }
}
