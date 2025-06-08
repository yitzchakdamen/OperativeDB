namespace OperativeDB
{
    class Agent
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string RealName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int MissionsCompleted { get; set; }

        public class Builder
        {
            private Agent agent = new Agent();

            public Builder SetId(int id)
            {
            agent.Id = id;
            return this;
            }

            public Builder SetCodeName(string codeName)
            {
            agent.CodeName = codeName;
            return this;
            }

            public Builder SetRealName(string realName)
            {
            agent.RealName = realName;
            return this;
            }

            public Builder SetLocation(string location)
            {
            agent.Location = location;
            return this;
            }

            public Builder SetStatus(string status)
            {
            agent.Status = status;
            return this;
            }

            public Builder SetMissionsCompleted(int missionsCompleted)
            {
            agent.MissionsCompleted = missionsCompleted;
            return this;
            }

            public Agent Build()
            {
            return agent;
            }
        }
    }

}