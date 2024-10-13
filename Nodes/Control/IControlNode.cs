using System;
using System.Collections.Generic;
using System.Text;

namespace FluentBehaviour.Nodes
{
    public interface IControlNode : INodeBase
    {
        public IControlNode AddChild(INodeBase node);
    }
}
