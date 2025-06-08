using MySql.Data.MySqlClient;

namespace OperativeDB
{
    class AgentDAL // : IDal
    {
        private MySqlData _MySqlData;
        public AgentDAL(MySqlData mySqlData)
        {
            _MySqlData = mySqlData;
        }

        public List<Agent> GetAll(string Query = "SELECT * FROM `agents`;")
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlDataReader reader;
            List<Agent> agents = new();

            reader = new MySqlCommand(Query, coon).ExecuteReader();

            while (reader.Read())
            {
                Agent agent = new Agent.Builder().SetId(reader.GetInt32("id")).SetCodeName(reader.GetString("codeName"))
                    .SetLocation(reader.GetString("location")).SetRealName(reader.GetString("realName"))
                    .SetStatus(reader.GetString("status")).SetMissionsCompleted(reader.GetInt32("missionsCompleted"))
                    .Build();
                agents.Add(agent);
            }
            coon.Close();
            return agents;
        }

        public void Add(Agent a)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO agents (codeName, realName, location, status, missionsCompleted)
             VALUES (@codeName, @realName, @location, @status, @missionsCompleted);";

            cmd.Parameters.AddWithValue("@codeName",a.CodeName);
            cmd.Parameters.AddWithValue("@realName",a.RealName);
            cmd.Parameters.AddWithValue("@location",a.Location);
            cmd.Parameters.AddWithValue("@status",a.Status);
            cmd.Parameters.AddWithValue("@missionsCompleted",a.MissionsCompleted);

            cmd.ExecuteNonQuery();
            coon.Close();
        }

        public void UpdateLocation(int agentId, string newLocation)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            cmd.CommandText = @"UPDATE agents SET location = @newLocation  WHERE agents.id = @agentId;";
            cmd.Parameters.AddWithValue("@newLocation", newLocation);
            cmd.Parameters.AddWithValue("@agentId", agentId);

            cmd.ExecuteNonQuery();
            coon.Close();
        }

        public void Delete(int agentId)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            string Query = $"DELETE FROM agents WHERE agents.id = {agentId};";
            new MySqlCommand(Query, coon).ExecuteNonQuery();
            coon.Close();
        }

        public List<Agent> SearchAgentsByCode(string partialCode)
        {
            return GetAll($"SELECT * FROM agents WHERE agents.codeName = '{partialCode}';");
        }

        public Dictionary<string, int> CountAgentsByStatus()
        {
            Dictionary<string, int> CountAgentsByStatus = new();
            string Query = "SELECT agents.status, COUNT(*) as 'sum status'  FROM `agents` GROUP BY status;";
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlDataReader reader;

            reader = new MySqlCommand(Query, coon).ExecuteReader();

            while (reader.Read())
            {
                string status = reader.GetString("status");
                int count = reader.GetInt32("sum status");
                CountAgentsByStatus[status] = count;
            }
            coon.Close();
            return CountAgentsByStatus;
        }

    }


}