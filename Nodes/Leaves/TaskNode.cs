using System;

namespace FluentBehaviour.Nodes
{
    public class TaskNode : INodeBase
    {
        public string Name { get; set; }
        public Func<TimeData, NodeStatus> Task { get; private set; }

        public TaskNode(string name, Func<TimeData, NodeStatus> task)
        {
            Name = name;
            Task = task;
        }

        public NodeStatus Tick(TimeData time)
        {
            return Task(time);
        }
    }
}
