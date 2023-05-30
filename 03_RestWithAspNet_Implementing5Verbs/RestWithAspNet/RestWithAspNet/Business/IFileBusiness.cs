using Microsoft.AspNetCore.Http;
using RestWithAspNet.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(List<IFormFile> files);
    }
}
