using prmToolkit.XLog.Domain.Enum;
using System;
using System.Threading.Tasks;

namespace prmToolkit.XLog.Domain.Interfaces
{
    public interface ILog : IDisposable
    {
        void Save(string message, EnumMessageType enumMessageType);

        void SaveAsync(string message, EnumMessageType enumMessageType);
    }
}
