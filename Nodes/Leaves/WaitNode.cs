namespace FluentBehaviour.Nodes
{
    public class WaitNode : INodeBase
    {
        public string Name { get; set; }
        public float WaitTime { get; set; }
        private double startTime;

        public WaitNode(string name, float seconds)
        {
            Name = name;
            WaitTime = seconds;
            startTime = 0;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (time.TotalTime - startTime >= WaitTime)
            {
                startTime = time.TotalTime;
                return NodeStatus.Success;
            }
            else
            {
                return NodeStatus.Running;
            }
        }
    }
}
