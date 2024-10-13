using FluentBehaviour.Nodes;
using System;

namespace FluentBehaviour
{
    [Serializable]
    public class Behaviour
    {
        public string Name { get; set; }
        public IControlNode Root { get; private set; }

        public NodeStatus Tick(float deltaTime)
        {
            return Root.Tick(deltaTime);
        }

        public Behaviour(string name, IControlNode root)
        {
            Name = name;
            Root = root;
        }
    }
}
