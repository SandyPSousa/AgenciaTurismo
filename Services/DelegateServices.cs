using AgenciaTurismo.Models;

namespace AgenciaTurismo.Services
{
    //Delegate para Cálculo de Descontos
    public delegate decimal CalculateDelegate(decimal valor, decimal percentualDesconto);

    // Delegate para logging
    public delegate void LogDelegate(string message);

    public class DelegateServices
    {
        // Lista para armazenar logs em memória
        private static List<string> _memoryLogs = new List<string>();

        // Implementaçao do cálculo de desconto
        public static decimal CalcularDesconto(decimal valor, decimal percentualDesconto)
        {
            return valor - (valor * percentualDesconto / 100);
        }

        // Metodos para o multicast delegate
        public static void LogToConsole(string message)
        {
            Console.WriteLine($"[CONSOLE] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        public static void LogToFile(string message)
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
            Directory.CreateDirectory(logPath);

            var fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
            var fullPath = Path.Combine(logPath, fileName);

            File.AppendAllText(fullPath, $"[FILE] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
        }

        public static void LogToMemory(string message)
        {
            _memoryLogs.Add($"[MEMORY] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");

            // Limitar a 100 entradas para não consumir tanta memória
            if (_memoryLogs.Count > 100)
            {
                _memoryLogs.RemoveAt(0);
            }
        }

        public static List<string> GetMemoryLogs() => _memoryLogs;

        //Func para cálculo de valor total
        public static Func<int, decimal, decimal> CalcularValorTotal = (numeroDiarias, valorDiaria) =>
        {
            return numeroDiarias * valorDiaria;
        };

        //Gerenciador de eventos para capacidade
        public static void SubscribeToCapacityEvent(PacoteTuristico pacote)
        {
            pacote.CapacityReached += (message) =>
            {
                LogDelegate logger = LogToConsole;
                logger += LogToFile;
                logger += LogToMemory;

                logger($"ALERTA DE CAPACIDADE: {message}");
            };
        }
    }
}