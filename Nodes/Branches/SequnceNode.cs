using System.Collections.Generic;

namespace FluentBehaviour.Nodes
{
    public class SequnceNode : IControlNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();

        public SequnceNode(string name)
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
            foreach (INodeBase child in children)
            {
                NodeStatus childStatus = child.Tick(time);

                if (childStatus == NodeStatus.Skip)
                {
                    continue;
                }
                else if (childStatus != NodeStatus.Success)
                {
                    return childStatus;
                }
            }
            return NodeStatus.Success;
        }
    }
}
