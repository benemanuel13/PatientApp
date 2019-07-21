using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Interfaces
{
    public interface IFileSystem
    {
        string GetBasePath();
        string GetViewModelsPath();
    }
}
