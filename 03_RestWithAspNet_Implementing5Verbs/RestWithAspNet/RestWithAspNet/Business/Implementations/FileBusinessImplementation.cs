using Microsoft.AspNetCore.Http;
using RestWithAspNet.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspNet.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _accessor;

        public FileBusinessImplementation(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetailVO = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _accessor.HttpContext.Request.Host;

            if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" 
                || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);

                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);

                    fileDetailVO.DocumentName = docName;
                    fileDetailVO.DocType = fileType;
                    fileDetailVO.DocURL = Path.Combine(baseUrl + "/api/file/v1" + fileDetailVO.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            return fileDetailVO;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(List<IFormFile> files)
        {
            List<FileDetailVO> fileDetailVOList = new List<FileDetailVO>();

            foreach(var file in files)
            {
                fileDetailVOList.Add(await SaveFileToDisk(file));
            }

            return fileDetailVOList;
        }


    }
}
