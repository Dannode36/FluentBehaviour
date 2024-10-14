namespace FluentBehaviour.Nodes
{
    public class Wait : INodeBase
    {
        public string Name { get; set; }
        public float WaitTime { get; set; }
        private float startTimestamp;

        public Wait(string name, float seconds)
        {
            Name = name;
            WaitTime = seconds;
            startTimestamp = 0;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (time.TotalTime - startTimestamp >= WaitTime)
            {
                startTimestamp = time.TotalTime;
                return NodeStatus.Success;
            }
            else
            {
                return NodeStatus.Running;
            }
        }
    }
}
