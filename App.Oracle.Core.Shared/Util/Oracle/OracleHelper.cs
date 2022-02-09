namespace App.Oracle.Core.Shared.Util.Oracle
{
    public class OracleHelper : IDisposable
    {
        public string ConnectionString { get; set; }

        public OracleHelper(string connectionString) => ConnectionString = connectionString;

        public OracleDataReader ExecuteReader(string spName)
        {
            OracleDataReader? reader = null;
            var oracleConnection = new OracleConnection(connectionString: ConnectionString);
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            try
            {
                oracleConnection.Open();
                oracleCommand.CommandText = spName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                reader = oracleCommand.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oracleConnection.Close();
            }
            return reader;
        }

        public OracleDataReader ExecuteReader(string spName, string[] paramNames, object[] paramValues)
        {
            OracleDataReader? reader = null;
            var oracleConnection = new OracleConnection(connectionString: ConnectionString);
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            try
            {
                oracleConnection.Open();
                oracleCommand.CommandText = spName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                for (var index = 0; index < paramNames.Length; ++index)
                {
                    oracleCommand.Parameters.Add(paramNames[index], paramValues[index]);
                }
                reader = oracleCommand.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oracleConnection.Close();
            }
            return reader;
        }

        public int ExecuteNonQuery(string spName, string[] paramNames, object[] paramValues)
        {
            int numberOfAffectedRows;
            var oracleConnection = new OracleConnection(connectionString: ConnectionString);
            OracleCommand oracleCommand = oracleConnection.CreateCommand();
            try
            {
                oracleConnection.Open();
                oracleCommand.CommandText = spName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                for (var index = 0; index < paramNames.Length; ++index)
                {
                    oracleCommand.Parameters.Add(paramNames[index], paramValues[index]);
                }
                numberOfAffectedRows = oracleCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oracleConnection.Close();
            }
            return numberOfAffectedRows;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
