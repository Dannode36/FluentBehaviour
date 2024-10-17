using System;

namespace FluentBehaviour.Nodes
{
    public class AlwaysDecorator : IBranchNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public AlwaysDecorator(string name)
        {
            Name = name;
        }

        public IBranchNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("ForceRunning cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("ForceRunning must have a child node!");
            }

            childNode.Tick(time);
            return NodeStatus.Running;
        }
    }
}