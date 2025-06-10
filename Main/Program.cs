using System.ComponentModel;

namespace OperativeDB
{
    class Program
    {
        static void Main()
        {
            Database mySqlData = new Database("eagleEyeDB").Connect();
            AgentDAL agentDAL = new(mySqlData);

            new Test().Run(agentDAL);

        }

        
    }
    
}