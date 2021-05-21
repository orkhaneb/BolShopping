using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BolShpping.Extensions.ImagesHelpers;
namespace BolShpping.Extensions
{
    public class FileUpload : IFileUpload
    {
        private readonly MyContext _context;
        public FileUpload(MyContext myContext)
        {
            _context = myContext;
        }
        public async Task<string> Create(string root, IFormFile file, string mainFolderName, string subFolderName, string Link)
        {
            string Kod = "";
            if (file != null)
            {
                try
                {
                    if (ImageIsValid(file))
                    {
                        string url = await ImageUploadAsync(root, file, mainFolderName, subFolderName);
                        Kod = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                        _context.Files.Add(new Models.BLL.File
                        {
                            FolderName = mainFolderName,
                            CreateDate = DateTime.Now,
                            LinkFile = Link,
                            Size = file.Length.ToString(),
                            UrlFile = url,
                            UniqKod = Kod,
                            Type = file.ContentType
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return Kod;
        }
        public async Task<string> Edit(string root, IFormFile file, string mainFolderName, string subFolderName, string Link, string UniqKod)
        {
            var delete = await _context.Files.Where(x => x.UniqKod == UniqKod).ToListAsync();
            foreach (var item in delete)
            {
                _context.Files.Remove(item);
                DeleteImage(item.UrlFile, mainFolderName);
            }
            await _context.SaveChangesAsync();
            string newadd = await Create(root, file, mainFolderName, subFolderName, Link);
            return newadd;
        }
        public async Task<string> Delete(string UniqKod, string FolderName)
        {
            var delete = await _context.Files.Where(x => x.UniqKod == UniqKod).ToListAsync();
            foreach (var item in delete)
            {
                DeleteImage(item.UrlFile, FolderName);
                _context.Files.Remove(item);

            }
            await _context.SaveChangesAsync();
            return "OK";
        }
    }
}
