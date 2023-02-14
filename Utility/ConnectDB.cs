using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace StudentResults
{
    internal class ConnectDB
    {
        internal NpgsqlConnection connection { get;}

        public ConnectDB()
        {
            this.connection = new NpgsqlConnection();
        }

        /// <summary>
        /// Opens connection to database and returns the current instance
        /// </summary>
        /// <returns>ConnectDB</returns>
        public ConnectDB OpenConnection()
        {
            this.connection.ConnectionString = Constants._ConnectionString;
            this.connection.Open();
            return this;
        }

        /// <summary>
        /// Closes the current connection to database
        /// </summary>
        public void CloseConnection()
        {
            this.connection.Dispose();
            this.connection.Close();
        }

    }
}
