using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Data.Linq2SQL
{
    public partial class Recipient : IDataErrorInfo
    {
        string IDataErrorInfo.Error => "";

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return "";

                    case nameof(Id): return "";

                    case nameof(Name):
                        if (Name is null) return "Имя не определено (пустая ссылка на строку)";
                        if (Name.Length < 3) return "Длина имени не может быть меньше 3 символов";
                        if (Name.Length > 35) return "Длина имени не может быть больше 35 символов";

                        return "";

                    case nameof(Email): return "";
                }
            }
        }

        partial void OnNameChanging(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (value == string.Empty) throw new InvalidOperationException("Имя не может быть пустой строкой");
        }
    }
}
