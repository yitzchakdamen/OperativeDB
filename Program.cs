using System.ComponentModel;

namespace OperativeDB
{
    class Program
    {
        static void Main()
        {
            Database mySqlData = new Database("eagleEyeDB").Connect();
            AgentDAL agentDAL = new(mySqlData);

            Agent agent = new Agent.Builder()
                .SetCodeName("4")
                .SetLocation("isrel")
                .SetRealName("mose")
                .SetStatus("Active")
                .SetMissionsCompleted(5)
                .Build();

            agentDAL.Add(agent);
            agentDAL.Delete(6);
            agentDAL.UpdateLocation(5, "Jerusalem");
            Dictionary<string, int> dict = agentDAL.CountAgentsByStatus();


            var ListAgent = agentDAL.GetAll();
            var SearchAgentsByCode = agentDAL.SearchAgentsByCode("AGD5575555");

            ListAgent.Print();
            SearchAgentsByCode.Print();
            dict.Print();
            
        }

        
    }
    
}