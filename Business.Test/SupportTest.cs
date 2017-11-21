using Xunit;
using Support;

namespace Business.Test
{
    
    public class LoggerTest
    {
        private Logger _logger;

        public LoggerTest() 
        {
            _logger = new Logger();
        }

        [Fact]
        public void WithoutLogs() 
        {
            Assert.Equal("", _logger.Show());
        }

        [Fact]
        public void OneLog() 
        {
            _logger.Debug("Message 1");
            Assert.Equal("Debug: Message 1\n", _logger.Show());
        }

        [Fact]
        public void SeveralLogs() 
        {
            _logger.Debug("Message 1");
            _logger.Debug("Message 2");
            _logger.Debug("Message 3");
            Assert.Equal("Debug: Message 1\nDebug: Message 2\nDebug: Message 3\n", _logger.Show());
        }

        [Fact]
        public void SeveralLogTypes() 
        {
            _logger.Debug("Message 1");
            _logger.Error("Message 2");
            _logger.Debug("Message 3");
            Assert.Equal("Debug: Message 1\nError: Message 2\nDebug: Message 3\n", _logger.Show());
            Assert.Equal("Error: Message 2\n", _logger.Show(LogType.Error));
            Assert.Equal("Debug: Message 1\nDebug: Message 3\n", _logger.Show(LogType.Debug));
        }
    }
}