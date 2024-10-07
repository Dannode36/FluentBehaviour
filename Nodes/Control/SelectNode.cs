using System.Collections.Generic;

namespace fluentflow.Nodes.Control
{
    public class SelectNode : IControlNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public SelectNode(string name)
        {
            Name = name;
        }

        public void AddChild(INodeBase node)
        {
            children.Add(node);
        }

        public NodeStatus Tick(float deltaTime)
        {
            foreach (var child in children)
            {
                var childStatus = child.Tick(deltaTime);
                if (childStatus != NodeStatus.Failure)
                {
                    return childStatus;
                }
            }

            return NodeStatus.Failure;
        }
    }
}