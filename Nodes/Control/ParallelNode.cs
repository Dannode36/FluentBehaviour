using System.Collections.Generic;

namespace fluentflow.Nodes.Control
{
    public class ParallelNode : IControlNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public ParallelNode(string name)
        {
            Name = name;
        }

        public void AddChild(INodeBase node)
        {
            children.Add(node);
        }

        public NodeStatus Tick(float deltaTime)
        {
            bool hasChildFailed = false;

            foreach (INodeBase node in children)
            {
                if(node.Tick(deltaTime) == NodeStatus.Failure)
                {
                    hasChildFailed = true;
                }
            }

            return hasChildFailed ? NodeStatus.Failure : NodeStatus.Success;
        }
    }
}
