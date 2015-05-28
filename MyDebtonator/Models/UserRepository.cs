using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDebtonator.Helpers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDebtonator.Models
{
    [Serializable()]
    public class UserRepository : ObservableObject, ISerializable
    {
        #region Fields and Properties

        private List<UserModel> savedUsers;

        public List<UserModel> SavedUsers
        {
            get 
            { 
                return this.savedUsers; 
            }
            set 
            { 
                this.savedUsers = value;
                OnPropertyChanged("SavedUsers");
            }
        }

        #endregion

        #region Constructors

        public UserRepository()
        {
            savedUsers = new List<UserModel>();
        }

        public UserRepository(SerializationInfo info, StreamingContext context)
        {
            this.SavedUsers = (List<UserModel>)info.GetValue("SavedUsers.dat", typeof(List<UserModel>));
        }

        #endregion

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SavedUsers.dat", this.SavedUsers);
        }

        #endregion
    }
}
