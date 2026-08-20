using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFlowApp.Views;

namespace GFlowApp.Services.Window
{
    public interface IWindowService
    {
        Avalonia.Controls.Window GetMainWindow();

        Task ShowDialogAsync(Avalonia.Controls.Window dialog);

        void CloseDialog(Avalonia.Controls.Window dialog);

        Task<TResult?> ShowDialogAsync<TResult>(Avalonia.Controls.Window dialog) where TResult : class;
    }
}