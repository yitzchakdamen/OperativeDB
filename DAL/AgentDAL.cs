using MySql.Data.MySqlClient;

namespace OperativeDB
{
    class AgentDAL
    {
        private Database _MySqlData;
        public AgentDAL(Database database)
        {
            _MySqlData = database;
        }

        public List<Agent> GetAll()
        {
            string Query = "SELECT * FROM `agents`;";
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlDataReader reader;
            List<Agent> agents = new();


            try
            {
                reader = new MySqlCommand(Query, coon).ExecuteReader();

                while (reader.Read())
                {
                    agents.Add(ProduceAgent(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
            return agents;
        }

        public void Add(Agent a)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO agents (codeName, realName, location, status, missionsCompleted)
             VALUES (@codeName, @realName, @location, @status, @missionsCompleted);";

            cmd.Parameters.AddWithValue("@codeName", a.CodeName);
            cmd.Parameters.AddWithValue("@realName", a.RealName);
            cmd.Parameters.AddWithValue("@location", a.Location);
            cmd.Parameters.AddWithValue("@status", a.Status);
            cmd.Parameters.AddWithValue("@missionsCompleted", a.MissionsCompleted);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
        }

        public void UpdateLocation(int agentId, string newLocation)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            cmd.CommandText = @"UPDATE agents SET location = @newLocation  WHERE agents.id = @agentId;";
            cmd.Parameters.AddWithValue("@newLocation", newLocation);
            cmd.Parameters.AddWithValue("@agentId", agentId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
        }

        public void Delete(int agentId)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            cmd.CommandText = "DELETE FROM agents WHERE agents.id = @AgentId";
            cmd.Parameters.AddWithValue("@agentId", agentId);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
        }

        public List<Agent> SearchAgentsByCode(string Code)
        {
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlCommand cmd = coon.CreateCommand();
            MySqlDataReader reader;

            List<Agent> agents = new();

            cmd.CommandText = "SELECT * FROM agents WHERE agents.codeName = @Code";
            cmd.Parameters.AddWithValue("@Code", Code);

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    agents.Add(ProduceAgent(reader));
                }
                return agents;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
        }

        public Dictionary<string, int> CountAgentsByStatus()
        {
            Dictionary<string, int> CountAgentsByStatus = new();
            string Query = "SELECT agents.status, COUNT(*) as 'sum status'  FROM `agents` GROUP BY status;";
            MySqlConnection coon = _MySqlData.GetConnction();
            MySqlDataReader reader;

            try
            {
                reader = new MySqlCommand(Query, coon).ExecuteReader();

                while (reader.Read())
                {
                    string status = reader.GetString("status");
                    int count = reader.GetInt32("sum status");
                    CountAgentsByStatus[status] = count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                coon.Close();
            }
            return CountAgentsByStatus;
        }

        private Agent ProduceAgent(MySqlDataReader reader)
        {
            Agent agent = new Agent.Builder()
                    .SetId(reader.GetInt32("id"))
                    .SetCodeName(reader.GetString("codeName"))
                    .SetLocation(reader.GetString("location"))
                    .SetRealName(reader.GetString("realName"))
                    .SetStatus(reader.GetString("status"))
                    .SetMissionsCompleted(reader.GetInt32("missionsCompleted"))
                    .Build();

            return agent;
        }
    }
}