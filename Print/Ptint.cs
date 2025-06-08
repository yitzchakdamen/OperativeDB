namespace OperativeDB
{
    static class Ptint
    {
        static public void Print(this Agent agent)
        {
            Console.WriteLine($"id: {agent.Id} <<--->> Code Name {agent.CodeName}  <<--->> Real Name: {agent.RealName} <<--->> Location: {agent.Location} <<--->> Status: {agent.Status}  <<--->> Missions Completed:{agent.MissionsCompleted}");
        }

        static public void Print(this List<Agent> ListAgent)
        {
            foreach (Agent item in ListAgent)
            {
                item.Print();
            }

        }
        static public void Print(this Dictionary<string, int> Dictionary)
        {
            foreach (var item in Dictionary)
            {
                Console.WriteLine($"Status: {item.Key}, Count: {item.Value}");
            }

        }
                        
    }

}