
using System;

namespace FluentBehaviour.Nodes
{
    public class ProbabilityNode : IControlNode
    {
        public string Name { get; set; }
        public float Probabilty { get; set; }
        private INodeBase? childNode;
        private readonly Random random;

        /// <summary>
        /// Excecutes child notes if the probability occurs
        /// </summary>
        /// <param name="name"></param>
        /// <param name="probabilty">Probability between 0 and 1</param>
        public ProbabilityNode(string name, float probabilty)
        {
            Name = name;
            Probabilty = probabilty;
            random = new Random();
        }

        public IControlNode AddChild(INodeBase node)
        {
            if (childNode != null)
            {
                throw new Exception("ProbabilityNode cannot have more than one child");
            }

            childNode = node;
            return this;
        }

        public NodeStatus Tick(TimeData time)
        {
            if (childNode == null)
            {
                throw new Exception("ProbabilityNode must have a child node!");
            }

            //If random value is not within the probability range
            if (random.NextDouble() > Probabilty)
            {
                return NodeStatus.Running;
            }

            return childNode.Tick(time);
        }
    }
}
