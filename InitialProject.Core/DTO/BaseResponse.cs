namespace Ecommerce.Core.DTO;

public class BaseResponse
{
    public bool Status { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public object Data { get; set; }
}