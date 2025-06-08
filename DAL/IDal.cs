using MySql.Data.MySqlClient;

namespace OperativeDB
{
    interface IDal
    {
        // Methods
        void Add(Agent agent);
        List<Agent> GetAll(MySqlConnection coon, string Query);
        void Update(int agentId, string newLocation);
        void Delete(int agentId);
    }

}