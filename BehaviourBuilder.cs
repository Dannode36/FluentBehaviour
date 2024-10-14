using System;
using System.Collections.Generic;
using FluentBehaviour.Nodes;

namespace FluentBehaviour
{
    public class BehaviourBuilder
    {
        /// <summary>
        /// Represents the last node added to the tree (after End() was called)
        /// </summary>
        IControlNode? lastPoppedNode;
        Stack<IControlNode> controlNodeStack = new Stack<IControlNode>();

        //Task node builders
        public BehaviourBuilder Task(string name, Func<TimeData, NodeStatus> task)
        {
            if(controlNodeStack.Count > 0)
            {
                controlNodeStack.Peek().AddChild(new TaskNode(name, task));
            }
            else
            {
                throw new Exception("TaskNode must be a child of a control node");
            }

            return this;
        }
        public BehaviourBuilder Condition(string name, Func<TimeData, bool> fn)
        {
            return Task(name, t => fn(t) ? NodeStatus.Success : NodeStatus.Failure);
        }
        public BehaviourBuilder Wait(string name, float seconds)
        {
            if (controlNodeStack.Count > 0)
            {
                controlNodeStack.Peek().AddChild(new Wait(name, seconds));
            }
            else
            {
                throw new Exception("TaskNode must be a child of a control node");
            }

            return this;
        }

        //Control node builders
        public BehaviourBuilder Sequence(string name)
        {
            AddControlNodeToStack(new SequnceNode(name));
            return this;
        }
        public BehaviourBuilder Select(string name)
        {
            AddControlNodeToStack(new SelectNode(name));
            return this;
        }
        public BehaviourBuilder Parallel(string name)
        {
            AddControlNodeToStack(new ParallelNode(name));
            return this;
        }
        public BehaviourBuilder Invert(string name)
        {
            AddControlNodeToStack(new Invert(name));
            return this;
        }

        //Builder functions
        public BehaviourBuilder Merge(IControlNode subTree)
        {
            AddControlNodeToStack(subTree);
            return this;
        }

        /// <summary>
        /// Ends a chain of child nodes
        /// </summary>
        public BehaviourBuilder End()
        {
            lastPoppedNode = controlNodeStack.Pop();
            return this;
        }

        /// <summary>
        /// Build the behaviour
        /// </summary>
        public Behaviour Build()
        {
            return Build("New Behaviour");
        }
        public Behaviour Build(string name)
        {
            if(lastPoppedNode == null)
            {
                throw new ApplicationException("Can't build a behaviour tree without any nodes");
            }
            else
            {
                if (controlNodeStack.Count > 0)
                {
                    throw new ApplicationException("Call End() before building the tree");
                }
                return new Behaviour(name, lastPoppedNode);
            }
        }

        //Helpers
        private void AddControlNodeToStack(IControlNode node)
        {
            if (controlNodeStack.Count > 0)
            {
                controlNodeStack.Peek().AddChild(node);
            }

            controlNodeStack.Push(node);
        }
    }
}
