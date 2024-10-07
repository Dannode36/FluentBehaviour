using fluentflow;
using fluentflow.Nodes;
using System;
using Newtonsoft.Json;
using System.Reflection;

namespace FluentBehaviour
{
    public static class SerializationExtensions
    {
        public static string? ToJson(this BehaviourBuilder builder)
        {
            return ToJson(builder.Build());
        }

        public static string? ToJson(this IControlNode node)
        {
            return JsonConvert.SerializeObject(node);
        }

        public static BehaviourBuilder FromJson(this BehaviourBuilder builder, string json)
        {
            return builder.Merge(JsonConvert.DeserializeObject<IControlNode>(json) ?? throw new Exception("Invalid JSON"));
        }

        public static IControlNode FromJson(this IControlNode node, string json)
        {
            return JsonConvert.DeserializeObject<IControlNode>(json) ?? throw new Exception("Invalid JSON");
        }
    }
}
