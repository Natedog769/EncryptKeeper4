using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEncryptKeeper4.Common;

namespace TheEncryptKeeper4.Models
{
    public class LoginEntry: BaseModel
    {
        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public int ID { get; set; }

        private string website;
        public string Website
        {
            get => website;
            set
            {
                website = value;
                OnPropertyChanged(nameof(Website));
            }
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string notes;
        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public void ClearValues()
        {
            Website = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            Notes = string.Empty;
        }

    }
}
