using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    /// Part of Users selection table.
    /// </summary>
    public class UserGridElement
    {
        /// <summary>
        /// Login of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Indicates if will be authorized or not.
        /// </summary>
        public bool IsChecked { get; set; }

        public UserGridElement(string username)
        {
            UserName = username;
        }

        /// <summary>
        /// Equals by type then by username.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
