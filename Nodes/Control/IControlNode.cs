using System;
using System.Collections.Generic;
using System.Text;

namespace fluentflow.Nodes
{
    public interface IControlNode : INodeBase
    {
        public void AddChild(INodeBase node);
    }
}
