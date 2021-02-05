using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp_PARDO
{
    public interface iToastService
    {
        void Show(string message, bool isLong);
    }
}
