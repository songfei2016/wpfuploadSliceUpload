using St.Upload.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St.Model;
using St.Common;

namespace St.Upload.Service
{
    public interface IApiService
    {
        Task<ResponseResult> GetToken(string userName, string password);

        Task<ResponseResult> Check(string token);

        Task<ResponseResult> GetResource(string token, int id);

        Task<ResourcePagedResultModel> GetResourcePaged(string token, int pageIndex = 1, int pageSize = 10, string query = "",
           string sortby = "addtime", string sortbyType = "desc");

        CreateOrUpdateResultModel CreateResource(string token, string name, string fileName, string description);

        CreateOrUpdateResultModel UpdateResource(string token, int id, string name, string fileName, string description);

        Task<ResponseResult> UpdateUpload(string token, int id, string path);

    }
}
