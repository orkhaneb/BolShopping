using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Extensions
{
    public interface IFileUpload
    {
        Task<string> Create(string root, IFormFile file, string mainFolderName, string subFolderName, string Link);
        Task<string> Edit(string root, IFormFile file, string mainFolderName, string subFolderName, string Link, string UniqKod);
        Task<string> Delete(string UniqKod, string FolderName);

    }
}
