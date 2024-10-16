using System;

namespace FluentBehaviour.Nodes
{
    public class ForceSuccessNode : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public ForceSuccessNode(string name)
        {
            Name = name;
        }

        public IControlNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("ForceSuccess cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("ForceSuccess must have a child node!");
            }

            childNode.Tick(time);
            return NodeStatus.Success;
        }
    }
}