using BeCoreApp.Application.ViewModels.Common;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
