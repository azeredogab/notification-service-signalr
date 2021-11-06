using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Infra.SignalR
{
    public static class ConnectionManager
    {
        private static List<Connection> connections = new List<Connection>();

        public static List<Connection> GetAll()
        {
            return connections; 
        }

        public static void Connect(Connection connection)
        {
            connections.Add(connection);
        }

        public static void Disconnect(string connectionId)
        {
            connections.RemoveAll(x => x.ConnectionId == connectionId);
        }

        public static Connection GetConnectionByUserAlias(string userAlias)
        {
            return connections.FirstOrDefault(x => x.User.Alias == userAlias); 
        }
    }
}
