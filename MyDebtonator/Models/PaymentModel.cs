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
    public class PaymentModel : ObservableObject, ISerializable, IComparer<PaymentModel>
    {
        #region Fields

        private DateTime _paymentDueDate;
        private Double _paymentAmount;

        #endregion

        #region Properties

        public DateTime PaymentDueDate
        {
            get 
            { 
                return this._paymentDueDate; 
            }
            set
            {
                this._paymentDueDate = value;
                OnPropertyChanged("PaymentDueDate");
            }
        }

        public Double PaymentAmount
        {
            get
            {
                return this._paymentAmount;
            }
            set
            {
                this._paymentAmount = value;
                OnPropertyChanged("PaymentAmount");
            }
        }

        #endregion

        #region Constructors

        public PaymentModel()
        {
            this.PaymentDueDate = DateTime.MinValue;
            this.PaymentAmount = 0.0;
        }

        public PaymentModel(DateTime date, Double amount)
        {
            this.PaymentDueDate = date;
            this.PaymentAmount = amount;
        }

        public PaymentModel(SerializationInfo info, StreamingContext context)
        {
            this.PaymentDueDate = (DateTime)info.GetValue("PaymentDueDate", typeof(DateTime));
            this.PaymentAmount = (Double)info.GetValue("PaymentAmount", typeof(Double));
        }

        #endregion

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PaymentDueDate", this.PaymentDueDate);
            info.AddValue("PaymentAmount", this.PaymentAmount);
        }

        #endregion

        #region IComparer Members

        public int Compare(PaymentModel x, PaymentModel y)
        {
            return DateTime.Compare(x.PaymentDueDate, y.PaymentDueDate);
        }

        #endregion
    }
}
