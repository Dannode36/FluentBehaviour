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

        public NodeStatus Tick(TimeData time)
        {
            bool hasChildFailed = false;

            foreach (INodeBase node in children)
            {
                NodeStatus childStatus = node.Tick(time);

                if(childStatus == NodeStatus.Skip)
                {
                    continue;
                }
                else if (node.Tick(time) == NodeStatus.Failure)
                {
                    hasChildFailed = true;
                }
            }

            return hasChildFailed ? NodeStatus.Failure : NodeStatus.Success;
        }
    }
}
