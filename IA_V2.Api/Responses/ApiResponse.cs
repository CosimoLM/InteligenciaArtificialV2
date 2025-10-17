namespace IA_V2.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}
