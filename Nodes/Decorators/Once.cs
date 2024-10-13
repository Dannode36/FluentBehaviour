using System;

namespace FluentBehaviour.Nodes
{
    public class Once : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;
        private bool hasRun;

        public Once(string name)
        {
            Name = name;
            hasRun = false;
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

            if (hasRun)
            {
                return NodeStatus.Success;
            }

            childNode.Tick(deltaTime);
            hasRun = true;
            return NodeStatus.Failure;
        }
    }
}