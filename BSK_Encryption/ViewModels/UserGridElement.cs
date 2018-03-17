using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.ViewModels
{
    public class UserGridElement
    {
        public string UserName { get; set; }
        public bool IsChecked { get; set; }

        public UserGridElement(string username)
        {
            UserName = username;
        }
        
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var b = obj as UserGridElement;

            return UserName.Equals(b.UserName);
        }
    }
}
