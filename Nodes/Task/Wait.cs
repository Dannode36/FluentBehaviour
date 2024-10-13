using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FluentBehaviour.Nodes
{
    public class Wait : INodeBase
    {
        public string Name { get; set; }
        public float WaitTime { get; set; }
        private float tickedTime;

        public Wait(string name, float seconds)
        {
            Name = name;
            WaitTime = seconds;

        }

        public NodeStatus Tick(float deltaTime)
        {
            tickedTime += deltaTime;
            if(tickedTime < WaitTime)
            {
                return NodeStatus.Running;
            }
            else
            {
                return NodeStatus.Success;
            }
        }
    }
}
