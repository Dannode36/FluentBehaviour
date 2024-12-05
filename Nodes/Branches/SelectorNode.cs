using System.Collections.Generic;

namespace FluentBehaviour.Nodes
{
    public class SelectorNode : IBranchNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public SelectorNode(string name)
        {
            Name = name;
        }

        public IBranchNode AddChild(INodeBase node)
        {
            children.Add(node);
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            foreach (var child in children)
            {
                NodeStatus childStatus = child.Tick(time);

                if (childStatus == NodeStatus.Skip)
                {
                    continue;
                }
                else if (childStatus != NodeStatus.Failure)
                {
                    return childStatus;
                }
            }

            //All nodes failed or skipped
            return NodeStatus.Failure;
        }
    }
}