using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services
{
    public interface INodeSchemaProvider
    {
        string NodeType { get; }
        NodeTypeSchema GetSchema();
    }
}