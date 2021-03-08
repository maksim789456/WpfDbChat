using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WpfDbChat.Annotations;

namespace WpfDbChat
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ChatItem SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ChatItem> ChatItems { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private ChatItem _selectedItem;
        private string _text;
        private string _username;

        public ApplicationViewModel()
        {
            ChatItems = new ObservableCollection<ChatItem>();
            using (AppDbContext dbContext = new AppDbContext())
            {
                List<ChatItemDb> temp = dbContext.ChatItems.OrderByDescending(x => x.Date).ToList();
                foreach (var item in temp)
                {
                    ChatItems.Add(new ChatItem
                    {
                        Date = item.Date,
                        Username = item.Username,
                        Message = item.Message
                    });
                }
            }
        }
        
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                       {
                           ChatItem chatItem = new ChatItem {Date = DateTime.Now, Username = _username, Message = _text};
                           ChatItems.Insert(0, chatItem);
                           Text = "";

                           using (AppDbContext context = new AppDbContext())
                           {
                               context.ChatItems.Add(new ChatItemDb{Date = chatItem.Date, Username = chatItem.Username, Message = chatItem.Message});
                               context.SaveChanges();
                           }
                       }));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}