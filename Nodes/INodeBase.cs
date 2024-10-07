using System;
using System.Collections.Generic;
using System.Text;

namespace fluentflow.Nodes
{
    public interface INodeBase
    {
        public string Name { get; set; }
        NodeStatus Tick(float deltaTime);
    }
}
