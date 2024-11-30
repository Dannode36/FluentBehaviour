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
        private IBranchNode? lastPoppedNode;
        private readonly Stack<IBranchNode> branchNodeStack = new Stack<IBranchNode>();

        //Task node builders
        public BehaviourBuilder Task(string name, Func<TimeData, NodeStatus> task)
        {
            if(branchNodeStack.Count > 0)
            {
                branchNodeStack.Peek().AddChild(new TaskNode(name, task));
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
            if (branchNodeStack.Count > 0)
            {
                branchNodeStack.Peek().AddChild(new WaitNode(name, seconds));
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
        public BehaviourBuilder RandomSelect(string name)
        {
            AddControlNodeToStack(new RandomSelectNode(name));
            return this;
        }
        public BehaviourBuilder Sequence(string name)
        {
            AddControlNodeToStack(new SequnceNode(name));
            return this;
        }
        public BehaviourBuilder Selector(string name)
        {
            AddControlNodeToStack(new SelectorNode(name));
            return this;
        }

        //Decorater node builders
        public BehaviourBuilder Always(string name)
        {
            AddControlNodeToStack(new AlwaysDecorator(name));
            return this;
        }
        public BehaviourBuilder Fail(string name)
        {
            AddControlNodeToStack(new FailDecorator(name));
            return this;
        }
        public BehaviourBuilder Succeed(string name)
        {
            AddControlNodeToStack(new SucceedDecorator(name));
            return this;
        }
        public BehaviourBuilder Invert(string name)
        {
            AddControlNodeToStack(new InvertDecorator(name));
            return this;
        }
        public BehaviourBuilder Once(string name, bool returnChildStatus = false)
        {
            AddControlNodeToStack(new OnceDecorator(name, returnChildStatus));
            return this;
        }
        public BehaviourBuilder Probability(string name, float probability)
        {
            AddControlNodeToStack(new ProbabilityDecorator(name, probability));
            return this;
        }
        public BehaviourBuilder Retry(string name, int retries)
        {
            AddControlNodeToStack(new RetryDecorator(name, retries));
            return this;
        }

        //Builder functions
        public BehaviourBuilder Merge(IBranchNode subTree)
        {
            AddControlNodeToStack(subTree);
            return this;
        }

        /// <summary>
        /// Ends a chain of child nodes
        /// </summary>
        public BehaviourBuilder End()
        {
            lastPoppedNode = branchNodeStack.Pop();
            return this;
        }

        /// <summary>
        /// Build the behaviour
        /// </summary>
        public Behaviour Build(string name)
        {
            if(lastPoppedNode == null || branchNodeStack.Count > 0)
            {
                throw new Exception("Call End() before Build()");
            }
            else
            {
                return new Behaviour(name, lastPoppedNode);
            }
        }

        //Helpers
        private void AddControlNodeToStack(IBranchNode node)
        {
            if (branchNodeStack.Count > 0)
            {
                branchNodeStack.Peek().AddChild(node);
            }

            branchNodeStack.Push(node);
        }
    }
}
