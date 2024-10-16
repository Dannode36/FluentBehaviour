using System;

namespace FluentBehaviour.Nodes
{
    public class OnceNode : IControlNode
    {
        public string Name { get; set; }
        public bool ReturnChildStatus { get; set; }
        private INodeBase? childNode;
        private NodeStatus childNodeStatus;
        private bool hasRun;

        public OnceNode(string name, bool returnChildStatus = false)
        {
            Name = name;
            hasRun = false;
            ReturnChildStatus = returnChildStatus;
        }

        public IControlNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("OnceNode cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("OnceNode must have a child node!");
            }

            if (hasRun)
            {
                return ReturnChildStatus ? childNodeStatus : NodeStatus.Skip;
            }

            childNodeStatus = childNode.Tick(time);
            hasRun = true;
            return childNodeStatus;
        }
    }
}
