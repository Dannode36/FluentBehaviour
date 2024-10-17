using System;

namespace FluentBehaviour.Nodes
{
    public class InvertDecorator : IBranchNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public InvertDecorator(string name)
        {
            Name = name;
        }

        public IBranchNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("InvertNode cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("InverterNode must have a child node!");
            }

            var result = childNode.Tick(time);

            if (result == NodeStatus.Failure)
            {
                return NodeStatus.Success;
            }
            else if (result == NodeStatus.Success)
            {
                return NodeStatus.Failure;
            }
            else
            {
                return result;
            }
        }
    }
}