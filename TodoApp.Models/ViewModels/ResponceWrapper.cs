
namespace TodoApp.Models
{
    public class ResponceWrapper
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Object Value {  get; set; }  
    }
}
