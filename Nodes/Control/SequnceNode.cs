using System.Collections.Generic;

namespace fluentflow.Nodes.Control
{
    public class SequnceNode : IControlNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public SequnceNode(string name)
        {
            Name = name;
        }

        public void AddChild(INodeBase node)
        {
            children.Add(node);
        }

        public NodeStatus Tick(float deltaTime)
        {
            foreach (INodeBase child in children)
            {
                NodeStatus childStatus = child.Tick(deltaTime);
                if (childStatus != NodeStatus.Success)
                {
                    return childStatus;
                }
            }
            return NodeStatus.Success;
        }
    }
}
