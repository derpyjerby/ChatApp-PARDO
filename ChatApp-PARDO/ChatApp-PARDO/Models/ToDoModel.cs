using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_PARDO
{
    class ToDoModel : INotifyPropertyChanged
    {
        int _id { get; set; }
        string _text { get; set; }
        bool _isDone { get; set; }

        public int id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string text { get { return _text; } set { _text = value; OnPropertyChanged(nameof(text)); } }
        public bool isDone { get { return _isDone; } set { _isDone = value; OnPropertyChanged(nameof(isDone)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
