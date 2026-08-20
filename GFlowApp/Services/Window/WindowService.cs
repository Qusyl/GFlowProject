using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services.Window
{
    public class WindowService : IWindowService
    {
        private Avalonia.Controls.Window? _mainWindow;

        public void SetMainWindow(Avalonia.Controls.Window window) => _mainWindow = window;
        public void CloseDialog(Avalonia.Controls.Window dialog)
        {
            dialog.Close();
        }

        public Avalonia.Controls.Window GetMainWindow()
        {
            return _mainWindow ?? throw new NullReferenceException("Window not set");
        }

        public async Task ShowDialogAsync(Avalonia.Controls.Window dialog)
        {
           if(_mainWindow is not null)
            {
               await dialog.ShowDialog(_mainWindow);
            }
            else
            {
                throw new InvalidOperationException("Not found any not empty windows");
            }
        }

        public async Task<TResult?> ShowDialogAsync<TResult>(Avalonia.Controls.Window dialog) where TResult : class
        {
            if (_mainWindow is null)
            {
                throw new InvalidOperationException("Not found any not empty windows");
            }
            return await dialog.ShowDialog<TResult>(_mainWindow);
        }
    }
}