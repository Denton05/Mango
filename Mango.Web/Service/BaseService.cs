using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service;

public class BaseService : IBaseService
{
    #region Fields

    private readonly IHttpClientFactory _httpClientFactory;

    #endregion

    #region Construction

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    #endregion

    #region Public Methods

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("MangoAPI");

            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            //TODO: token
            message.RequestUri = new Uri(requestDto.Url);

            if(requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            var apiResponse = new HttpResponseMessage();
            switch(requestDto.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);

            switch(apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new ResponseDto { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                case HttpStatusCode.BadRequest:
                    return new ResponseDto { IsSuccess = false, Message = "Bad Request" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch(Exception ex)
        {
            var dto = new ResponseDto
                      {
                          Message = ex.Message,
                          IsSuccess = false
                      };
            return dto;
        }
    }

    #endregion
}
