using System;
namespace  Application.Executor.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeExecutorAttribute : Attribute
    {
        public string Type;
        public NodeExecutorAttribute(string type) => Type = type;
    }
   
}