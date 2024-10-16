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
        private IControlNode? lastPoppedNode;
        private readonly Stack<IControlNode> controlNodeStack = new Stack<IControlNode>();

        //Task node builders
        public BehaviourBuilder Task(string name, Func<TimeData, NodeStatus> task)
        {
            if(controlNodeStack.Count > 0)
            {
                controlNodeStack.Peek().AddChild(new TaskNode(name, task));
            }
            else
            {
                throw new Exception("TaskNode must be a child of a control node or decorator");
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
                controlNodeStack.Peek().AddChild(new WaitNode(name, seconds));
            }
            else
            {
                throw new Exception("WaitNode must be a child of a control node or decorator");
            }

            return this;
        }

        //Control node builders
        public BehaviourBuilder Parallel(string name)
        {
            AddControlNodeToStack(new ParallelNode(name));
            return this;
        }
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

        //Decorater node builders
        public BehaviourBuilder ForceFailure(string name)
        {
            AddControlNodeToStack(new ForceFailureNode(name));
            return this;
        }
        public BehaviourBuilder ForceRunning(string name)
        {
            AddControlNodeToStack(new ForceRunningNode(name));
            return this;
        }
        public BehaviourBuilder ForceSuccess(string name)
        {
            AddControlNodeToStack(new ForceSuccessNode(name));
            return this;
        }
        public BehaviourBuilder Invert(string name)
        {
            AddControlNodeToStack(new InvertNode(name));
            return this;
        }
        public BehaviourBuilder Once(string name, bool returnChildStatus = false)
        {
            AddControlNodeToStack(new OnceNode(name, returnChildStatus));
            return this;
        }
        public BehaviourBuilder Retry(string name, int retries)
        {
            AddControlNodeToStack(new RetryNode(name, retries));
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
        public Behaviour Build(string name)
        {
            if(lastPoppedNode == null || controlNodeStack.Count > 0)
            {
                throw new Exception("Call End() before Build()");
            }
            else
            {
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
