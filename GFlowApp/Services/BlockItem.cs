using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFlowApp.ViewModels;

namespace GFlowApp.Services
{
    public record BlockItem(
    BlockViewModel Model,
    string Name);
}
