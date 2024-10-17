using System;
using System.Collections.Generic;
using System.Text;

namespace FluentBehaviour.Nodes
{
    public interface IBranchNode : INodeBase
    {
        public IBranchNode AddChild(INodeBase node);
    }
}
