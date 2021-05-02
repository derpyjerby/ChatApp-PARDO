using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_PARDO
{
    public class ConversationModel : INotifyPropertyChanged
    {
        string _id 
        {   get; 
            set; 
        }
        public string id 
        { 
            get 
            { 
                return _id; 
            } 
            set 
            { 
                _id = value; 
                OnPropertyChanged(nameof(id)); 
            } 
        }
        string _message { get; set; }
        public string message 
        { 
            get 
            { 
                return _message; 
            } 
            set 
            { 
                _message = value; 
                OnPropertyChanged(nameof(message)); 
            } 
        }

        string _converseeID { get; set; }
        public string converseeID { get { return _converseeID; } set { _converseeID = value; OnPropertyChanged(nameof(converseeID)); } }

        string _created_at
        {
            get;
            set;
        }

       public string created_at
        {
            get
            {
                return _created_at;
            }
            set
            {
                _created_at = value;
                OnPropertyChanged(nameof(created_at));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}