namespace ABC.Domain.Models
{

    public class ObjectResponse
    {
        public ObjectResponse()
        {
        }

        public ObjectResponse(bool successful,string resultMessage = "")
        {
            Successful = successful;
            ResultMessage = resultMessage;
        }
        public bool Successful { get; set; }
        public string ResultMessage { get; set; }
    }
}