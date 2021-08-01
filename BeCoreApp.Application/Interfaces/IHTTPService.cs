using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface IHttpService
    {
        Task<GenericResult> GetAsync(string url);
        Task<GenericResult> PostAsync(string url, object parrams);
    }
}
