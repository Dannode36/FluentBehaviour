using System;

namespace FluentBehaviour.Nodes
{
    public class ForceSuccess : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public ForceSuccess(string name)
        {
            Name = name;
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

            childNode.Tick(deltaTime);
            return NodeStatus.Success;
        }
    }
}