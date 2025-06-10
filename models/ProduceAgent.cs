using MySql.Data.MySqlClient;

namespace OperativeDB
{
    static class ProduceAgent
    {
        static public Agent Produce(MySqlDataReader reader)
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