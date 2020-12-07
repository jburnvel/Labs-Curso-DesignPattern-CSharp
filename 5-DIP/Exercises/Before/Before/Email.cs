namespace Before
{
    public class Email : IMessage
    {
        public string Subject { get; set; }
        public string Content { get; set; } 

        public void SendMessage()
        {
            throw new System.NotImplementedException();
        }
    }
}