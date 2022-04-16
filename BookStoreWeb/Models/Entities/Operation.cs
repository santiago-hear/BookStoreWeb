namespace BookStoreWeb.Models.Entities
{
    public class Operation
    {
        public string State { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public Operation(string state, string message, string response)
        {
            State = state;
            Message = message;
            Response = response;
        }
    }
}
