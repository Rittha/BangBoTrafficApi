using FluentModbus;
using System.Net;
using BangBoTrafficApi.Models;

namespace BangBoTrafficApi.Services
{

    public interface IPlcService
    {
        void SendSignal(string lane, string status);
    }

    public class PlcService : IPlcService
    {
        private readonly IConfiguration _config;

        public PlcService(IConfiguration config)
        {
            _config = config;
        }

        public void SendSignal(string lane, string status)
        {
            string plcIp = _config["PlcSettings:IpAddress"] ?? "192.168.1.10"; // IP PLC
            int port = int.Parse(_config["PlcSettings:Port"] ?? "502");

            try
            {
                using var client = new ModbusTcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(plcIp), port));

                // Mapping Address ตามทิศทางจริงจากแผนที่
                // A: บางพลี (Reg 0), B: ตลาดบางบ่อ (Reg 2), C: คลองด่าน (Reg 4)
                int address = lane switch { "A" => 0, "B" => 2, "C" => 4, _ => 0 };

                // Mapping Value (Red=0, Yellow=1, Green=2)
                short value = status switch { "Red" => 0, "Yellow" => 1, "Green" => 2, _ => 0 };

                client.WriteSingleRegister(0, (ushort)address, value);
            }
            catch (Exception ex)
            {
                // บันทึก Log ลง Console ของ Visual Studio
                Console.WriteLine($"[PLC ERROR] {DateTime.Now}: {ex.Message}");
            }
        }
    }
}