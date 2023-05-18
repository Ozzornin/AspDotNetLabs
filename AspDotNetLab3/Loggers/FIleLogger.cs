using System.Text;

namespace AspDotNetLab3.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        public FileLogger(IConfiguration config, IHttpContextAccessor accessor)
        {
            _configuration = config;
            _accessor = accessor;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string? ip = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            string? path = _accessor.HttpContext?.Request.Path; 
            string res = formatter.Invoke(state, exception);
            var sb = new StringBuilder();
            sb.AppendLine($"Date: {DateTime.Now}");
            sb.AppendLine($"Ip-address(may be empty): {ip}");
            sb.AppendLine($"Request path(may be empty): {path}");
            File.AppendAllText(_configuration["LogFile"], sb.ToString());

        } 
        
    }
}
