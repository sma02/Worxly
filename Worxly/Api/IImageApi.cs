using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.Api
{
    interface IImageApi
    {
        [Multipart]
        [Post("/api/Image")]
        Task<ApiResponse<string>> UploadImage([AliasAs("file")] StreamPart stream);
    }
}
