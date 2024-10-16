using System;

namespace FluentBehaviour.Nodes
{
    public class RetryNode : IControlNode
    {
        public string Name { get; set; }
        public int Retries { get; set; }
        private INodeBase? childNode;

        public RetryNode(string name, int retries)
        {
            Name = name;
            Retries = retries;
        }

        public IControlNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("RetryNode cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("RetryNode must have a child node!");
            }

            NodeStatus childStatus = childNode.Tick(time);

            if (childStatus == NodeStatus.Failure && Retries > 0)
            {
                Retries--; //Next tick will count as the retry
                return NodeStatus.Running;
            }

            return childStatus;
        }
    }
}
