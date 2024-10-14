using System;

namespace FluentBehaviour.Nodes
{
    public class Invert : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public Invert(string name)
        {
            Name = name;
        }

        public IControlNode AddChild(INodeBase node)
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