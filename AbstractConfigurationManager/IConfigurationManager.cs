using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AbstractConfigurationManager
{
    public interface IConfigurationManager
    {
        string AppSetting(string name);        
        NameValueCollection AppSettings { get;  }
    }
}
