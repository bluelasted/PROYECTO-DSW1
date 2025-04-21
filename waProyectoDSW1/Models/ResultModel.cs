namespace waProyectoDSW1.Models
{
    public class ResultModel<T>
    {
        public bool success { get; set; }
        public string Message {  get; set; }
        public T? Data { get; set; }
    }
}
