namespace WORKFLOW.Model.Shared
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Success";
        public int Pages { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}
