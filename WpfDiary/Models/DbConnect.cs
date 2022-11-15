using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDiary.Models
{
    public class DbConnect : IDataErrorInfo
    {
        public string Server { get; set; }
        public string ServerDbName { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }




        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Server):

                        if (string.IsNullOrWhiteSpace(Server))
                        {
                            Error = "Pole Server jest wymagane.";
                          
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;

                    default:
                        break;

                }
                return Error;
      
            }

        }

        public string Error { get; set; }
    }
}
