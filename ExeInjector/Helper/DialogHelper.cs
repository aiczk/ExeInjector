using Microsoft.Win32;

namespace ExeInjector.Helper
{
    internal static class DialogHelper
    {
        //todo Reusing Instances.
        internal static string SelectFileDialog(string filter)
        {
            var fileName = string.Empty;
            var dialog = new OpenFileDialog
            {
                Filter = filter,
            };
            
            if (dialog.ShowDialog() == true) 
                fileName = dialog.FileName;

            return fileName;
        }
    }
}