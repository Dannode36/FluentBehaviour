using FluentBehaviour.Nodes;
using System;

namespace FluentBehaviour
{
    [Serializable]
    public class Behaviour
    {
        public string Name { get; set; }
        public IBranchNode Root { get; private set; }
        private readonly TimeData time;

        public Behaviour(string name, IBranchNode root)
        {
            Name = name;
            Root = root;
            time = new TimeData();
        }

        public NodeStatus Tick(double deltaTime)
        {
            time.TotalTime += deltaTime;
            time.DeltaTime = deltaTime;
            return Root.Tick(time);
        }
    }
}
