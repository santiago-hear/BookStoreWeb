using System.Runtime.Serialization;

namespace BookStoreWeb.Models.Exceptions
{
    public class ConnectionErrorException : Exception
    {
        public ConnectionErrorException(string message) : base(message) { }
        protected ConnectionErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
