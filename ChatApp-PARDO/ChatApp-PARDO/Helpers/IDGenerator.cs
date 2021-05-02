using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ChatApp_PARDO
{
    public class IDGenerator
    {
        public static string generateID()
        {
            Guid guid = Guid.NewGuid();
            string str = guid.ToString();
            return str;
        }
    }
            
}
