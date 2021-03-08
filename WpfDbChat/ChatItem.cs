using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfDbChat.Annotations;

namespace WpfDbChat
{
    public class ChatItem : INotifyPropertyChanged
    {
        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }
        
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        private DateTime _date;
        private string _username;
        private string _message;
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}