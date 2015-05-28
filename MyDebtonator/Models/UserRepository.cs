using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<UserModel> savedUsers;

        public ObservableCollection<UserModel> SavedUsers
        {
            get 
            { 
                return this.savedUsers; 
            }
            set 
            { 
                this.savedUsers = value;
            }
        }

        #endregion

        #region Constructors

        public UserRepository()
        {
            savedUsers = new ObservableCollection<UserModel>();
        }

        public UserRepository(SerializationInfo info, StreamingContext context)
        {
            this.SavedUsers = (ObservableCollection<UserModel>)info.GetValue("SavedUsers.dat", typeof(ObservableCollection<UserModel>));
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
