using System.Collections.Generic;

namespace St.Model
{
    public class ApiBaseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class TokenData
    {
        public string AccessToken { get; set; }
    }


    public class CheckResultModel
    {
        public string Token { get; set; }

        public string FileUploadUrl { get; set; }
    }

    public class ResourcePagedResultModel
    {
        public int Total { get; set; }
        public List<ApiResourceModel> Items { get; set; }
    }

    public class CreateOrUpdateResultModel : ApiBaseModel
    {
        public ApiResourceModel Data { get; set; }
    }

}
