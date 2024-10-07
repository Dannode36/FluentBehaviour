using System;
using System.Collections.Generic;
using System.Text;

namespace fluentflow.Nodes
{
    public class TaskNode : INodeBase
    {
        public string Name { get; set; }
        public Func<float, NodeStatus> Task { get; private set; }

        public TaskNode(string name, Func<float, NodeStatus> task)
        {
            Name = name;
            Task = task;
        }

        public NodeStatus Tick(float deltaTime)
        {
            return Task(deltaTime);
        }
    }
}
