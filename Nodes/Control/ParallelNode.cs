using System.Collections.Generic;

namespace FluentBehaviour.Nodes
{
    public class ParallelNode : IControlNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public ParallelNode(string name)
        {
            Name = name;
        }

        public IControlNode AddChild(INodeBase node)
        {
            children.Add(node);
            return this;
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
