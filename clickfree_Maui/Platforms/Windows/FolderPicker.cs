﻿using clickfree_Maui.Contracts.Services;
using WindowsFolderPicker = Windows.Storage.Pickers.FolderPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Platforms.Windows
{
    public class FolderPicker : IFolderPicker
    {
        public async Task<string> PickFolder()
        {
            try
            {
                var folderPicker = new WindowsFolderPicker();

                folderPicker.FileTypeFilter.Add("*");

                // Get the current window's HWND by passing in the Window object
                var hwnd = ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;

                // Associate the HWND with the file picker
                WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

                var result = await folderPicker.PickSingleFolderAsync();
                if (result != null)
                {
                    return result.Path;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
