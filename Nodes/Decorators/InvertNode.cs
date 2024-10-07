using System;
using System.Collections.Generic;

namespace fluentflow.Nodes.Control
{
    public class InvertNode : IControlNode
    {
        public string Name { get; set; }
        private INodeBase? childNode;

        public InvertNode(string name)
        {
            Name = name;
        }

        public void AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("InvertNode cannot have more than one child");
            }

            childNode = node;
        }

        public NodeStatus Tick(float deltaTime)
        {
            if (childNode == null)
            {
                throw new Exception("InverterNode must have a child node!");
            }

            var result = childNode.Tick(deltaTime);

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