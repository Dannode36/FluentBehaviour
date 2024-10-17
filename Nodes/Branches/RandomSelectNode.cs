using System;
using System.Collections.Generic;

namespace FluentBehaviour.Nodes
{
    public class RandomSelectNode : IBranchNode
    {
        public string Name { get; set; }
        private List<INodeBase> children = new List<INodeBase>();
        private readonly Random random;

        public RandomSelectNode(string name)
        {
            Name = name;
            random = new Random();
        }

        public IBranchNode AddChild(INodeBase node)
        {
            children.Add(node);
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            int index = random.Next(children.Count);
            return children[index].Tick(time);
        }
    }
}