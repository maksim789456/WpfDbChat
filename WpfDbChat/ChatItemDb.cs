using System;

namespace WpfDbChat
{
    public class ChatItemDb
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
    }
}