using System;

namespace FluentBehaviour.Nodes
{
    public class FailDecorator : IBranchNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public FailDecorator(string name)
        {
            Name = name;
        }

        public IBranchNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("ForceFailure cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("ForceFailure must have a child node!");
            }

            childNode.Tick(time);
            return NodeStatus.Failure;
        }
    }
}