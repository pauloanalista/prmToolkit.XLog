using prmToolkit.XLog.Domain.Enum;
using prmToolkit.XLog.Domain.Extensions;
using prmToolkit.XLog.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace prmToolkit.XLog.SqlServer
{
    public sealed class Log : ILog
    {
        private readonly string _applicationName;
        private readonly string _connectionString;
        private SqlConnection _conn;

        public Log(string applicationName, string connectionString)
        {
            _applicationName = applicationName;
            _connectionString = connectionString;
            //_conn = new SqlConnection(connectionString);
            //_conn.Open();
        }

        public async void Save(string message, EnumMessageType enumMessageType = EnumMessageType.Information)
        {
            string sql = $"INSERT INTO log (Application, MessageType, Message, CurrentDate) VALUES (@ApplicationName, @MessageType, @Message, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";

            using (var _conn = new SqlConnection(_connectionString))
            {
                await _conn.OpenAsync();


                using (var tran = _conn.BeginTransaction())
                {
                    using (var command = new SqlCommand(sql, _conn, tran))
                    {
                        try
                        {
                            command.Parameters.Add(new SqlParameter()
                            {
                                DbType = DbType.String,
                                Direction = ParameterDirection.Input,
                                ParameterName = "@ApplicationName",
                                Value = _applicationName
                            });

                            command.Parameters.Add(new SqlParameter()
                            {
                                DbType = DbType.String,
                                Direction = ParameterDirection.Input,
                                ParameterName = "@Message",
                                Value = message
                            });
                            command.Parameters.Add(new SqlParameter()
                            {
                                DbType = DbType.String,
                                Direction = ParameterDirection.Input,
                                ParameterName = "@MessageType",
                                Value = enumMessageType.GetDescription()
                            });

                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw;
                        }
                        tran.Commit();
                    }
                }
            }
        }

        public void SaveAsync(string message, EnumMessageType enumMessageType = EnumMessageType.Information)
        {
            Task.Factory.StartNew(() =>
            {
                Save(message, enumMessageType);
            });
        }

        public void Dispose()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
                _conn.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}