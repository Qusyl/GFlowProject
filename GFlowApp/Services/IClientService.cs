using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Runtime;

namespace GFlowApp.Services
{
    public interface IClientService
    {
        Task<RuntimeResult?> ExecuteAsync(WorkflowDto dto);
        Task<NodeTypeSchema?> GetSchemaAsync(string NodeType);
    }
}