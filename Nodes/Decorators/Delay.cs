using System;
using System.Diagnostics;

namespace FluentBehaviour.Nodes
{
    public class Delay : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;
        private Stopwatch delayTimer;

        public Delay(string name, float seconds)
        {
            Name = name;
            delayTimer = false;
        }

        public IControlNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("AlwaysNode cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(float deltaTime)
        {
            if (childNode == null)
            {
                throw new Exception("AlwaysNode must have a child node!");
            }

            if (!delayTimer.IsRunning)
            {
                return NodeStatus.Running;
            }

            childNode.Tick(deltaTime);
            hasRun = true;
            return NodeStatus.Failure;
        }
    }
}