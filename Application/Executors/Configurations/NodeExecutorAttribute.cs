using System;
namespace  Application.Executors.Configurations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeExecutorAttribute : Attribute
    {
        public string Type;
        public NodeExecutorAttribute(string type) => Type = type;
    }
   
}