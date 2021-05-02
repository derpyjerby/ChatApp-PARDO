using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_PARDO
{
    public class ContactModel : INotifyPropertyChanged
    {
        string _id { get; set; }
        //[JsonProperty(PropertyName = "id")]
        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        
        string[] _contactID { get; set; }
        //[JsonProperty(PropertyName = "contactID")]
        public string[] contactID { get { return _contactID; } set { _contactID = value; OnPropertyChanged(nameof(contactID)); } }
        
        string[] _contactName { get; set; }
        //[JsonProperty(PropertyName = "contactName")]
        public string[] contactName { get { return _contactName; } set { _contactName = value; OnPropertyChanged(nameof(contactName)); } }

        string[] _contactEmail { get; set; }
        //[JsonProperty(PropertyName = "contactEmail")]
        public string[] contactEmail { get { return _contactEmail; } set { _contactEmail = value; OnPropertyChanged(nameof(contactEmail)); } }
        
        string _created_at { get; set; }
        //[JsonProperty(PropertyName = "created_at")]
        public string created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}