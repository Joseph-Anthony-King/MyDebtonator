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
    public class PaymentPlanModel : ObservableObject, ISerializable, IComparer<PaymentPlanModel>
    {
        #region Fields

        public bool _monthlyRateExceedsMonthlyPayment;

        private string _paymentPlanName;
        private Double _principal;
        private Double _interest;
        private Double _totalLoanPayments;
        private DateTime _firstPaymentDueDate;
        private DateTime _lastPaymentDueDate;
        private Double _monthlyPayment;
        private Double _finalPayment;
        private Double _apr;
        private Double _monthlyRate;
        private int _months;
        private ObservableCollection<PaymentModel> _paymentSchedule;

        #endregion

        #region Properties

        public string PaymentPlanName
        {
            get
            {
                return this._paymentPlanName;
            }
            set
            {
                this._paymentPlanName = value;
                OnPropertyChanged("PaymentPlanName");
            }
        }

        public Double Principal
        {
            get
            {
                return this._principal;
            }
            set
            {
                this._principal = value;
                OnPropertyChanged("Principal");
            }
        }

        public Double Interest
        {
            get
            {
                return this._interest;
            }
            set
            {
                this._interest = value;
                OnPropertyChanged("Interest");
            }
        }

        public Double TotalLoanPayments
        {
            get
            {
                return this._totalLoanPayments;
            }
            set
            {
                this._totalLoanPayments = value;
                OnPropertyChanged("TotalLoanPayments");
            }
        }

        public DateTime FirstPaymentDueDate
        {
            get
            {
                return this._firstPaymentDueDate;
            }
            set
            {
                this._firstPaymentDueDate = value;
                OnPropertyChanged("FirstPaymentDueDate");
            }
        }

        public DateTime LastPaymentDueDate
        {
            get
            {
                return this._lastPaymentDueDate;
            }
            set
            {
                this._lastPaymentDueDate = value;
                OnPropertyChanged("LastPaymentDueDate");
            }
        }

        public Double MonthlyPayment
        {
            get
            {
                return this._monthlyPayment;
            }
            set
            {
                this._monthlyPayment = value;
                OnPropertyChanged("MonthlyPayment");
            }
        }

        public Double FinalPayment
        {
            get
            {
                return this._finalPayment;
            }
            set
            {
                this._finalPayment = value;
                OnPropertyChanged("FinalPayment");
            }
        }

        public Double APR
        {
            get
            {
                return this._apr;
            }
            set
            {
                this._apr = value;
                OnPropertyChanged("APR");
            }
        }

        public Double MonthlyRate
        {
            get
            {
                return this._monthlyRate;
            }
            set
            {
                this._monthlyRate = value;
                OnPropertyChanged("MonthlyRate");
            }
        }

        public int Months
        {
            get
            {
                return this._months;
            }
            set
            {
                this._months = value;
                OnPropertyChanged("Months");
            }
        }

        public ObservableCollection<PaymentModel> PaymentSchedule
        {
            get
            {
                return this._paymentSchedule;
            }
            set
            {
                this._paymentSchedule = value;
            }
        }

        #endregion

        #region Constructors

        public PaymentPlanModel()
        {
            this.PaymentPlanName = String.Empty;

            this.Principal = 0.0;
            this.Interest = 0.0;
            this.TotalLoanPayments = 0.0;

            this.FirstPaymentDueDate = new DateTime(2000, 1, 1);

            this.MonthlyPayment = 0.0;
            this.FinalPayment = 0.0;

            this.APR = 0.0;
            this.MonthlyRate = 0.0;

            this.Months = 0;

            this._monthlyRateExceedsMonthlyPayment = false;

            this.PaymentSchedule = new List<PaymentModel>();
        }

        public PaymentPlanModel(string name, Double principal, Double apr, DateTime firstPaymentDate) : 
            this()
        {
            this.PaymentPlanName = name;

            this.Principal = principal;

            this.FirstPaymentDueDate = firstPaymentDate;

            this.APR = apr;

            this.MonthlyRate = APR / 12;
        }

        public PaymentPlanModel(string name, Double principal, Double apr, DateTime firstPaymentDate, int months) : 
            this(name, principal, apr, firstPaymentDate)
        {
            Months = months;
        }

        public PaymentPlanModel(string name, Double principal, Double apr, DateTime firstPaymentDate, Double monthlyPayment) :
            this(name, principal, apr, firstPaymentDate)
        {
            MonthlyPayment = monthlyPayment;
        }

        public PaymentPlanModel(SerializationInfo info, StreamingContext context)
        {
            this.PaymentPlanName = (string)info.GetValue("PaymentPlanName", typeof(string));
            this.Principal = (Double)info.GetValue("Principal", typeof(Double));
            this.Interest = (Double)info.GetValue("Interest", typeof(Double));
            this.TotalLoanPayments = (Double)info.GetValue("TotalLoanPayments", typeof(Double));
            this.FirstPaymentDueDate = (DateTime)info.GetValue("FirstPaymentDueDate", typeof(DateTime));
            this.LastPaymentDueDate = (DateTime)info.GetValue("LastPaymentDueDate", typeof(DateTime));
            this.MonthlyPayment = (Double)info.GetValue("MonthlyPayment", typeof(Double));
            this.FinalPayment = (Double)info.GetValue("FinalPayment", typeof(Double));
            this.APR = (Double)info.GetValue("APR", typeof(Double));
            this.MonthlyRate = (Double)info.GetValue("MonthlyRate", typeof(Double));
            this.Months = (int)info.GetValue("Months", typeof(int));
            this._monthlyRateExceedsMonthlyPayment = (bool)info.GetValue("_montlyRateExceedsMonthlyPayment", typeof(bool));
            this.PaymentSchedule = (List<PaymentModel>)info.GetValue("PaymentSchedule", typeof(List<PaymentModel>));
        }

        #endregion

        #region Methods

        /* If the monthly payment is provided this method calculates the amount of months
         * it will take to pay this loan off.
         */
        public void CalculateMonths()
        {
            _monthlyRateExceedsMonthlyPayment = MontlyInterestRateExceedsMontlyPayment();

            if (!_monthlyRateExceedsMonthlyPayment) // Do not continue if monthly rate is not sufficient
            {
                Double balance = Principal; // Local variable to keep track of the balance within the method.

                for (Months = 0; balance != 0; Months++)
                {
                    if (balance > MonthlyPayment)
                    {
                        balance = balance - MonthlyPayment;
                        balance = balance * (1.0 + MonthlyRate);
                        balance = Math.Round(FinalPayment, 2);
                    }
                    else
                    {
                        FinalPayment = balance * (1.0 + MonthlyRate);
                        FinalPayment = Math.Round(FinalPayment, 2);
                        balance = 0;
                    }
                }

                for (int i = 0; i < Months - 1; i++)
                {
                    Interest = Interest + MonthlyPayment;
                }

                Interest = Interest + FinalPayment;

                Interest = Interest - Principal;

                TotalLoanPayments = Principal + Interest;

                PaymentSchedule = CreatePaymentSchedule();
            }
        }

        // If the months are provided this method will calculate the monthly payment.
        public void CalculatePayments()
        {
            if (APR != 0)
            {
                MonthlyPayment = (Principal * MonthlyRate) / (1 - Math.Pow(1.0 + MonthlyRate, -Months));
            }
            else // This code prevents the value of MonthlyPayment from equalling Nan if APR is 0.0%.
            {
                MonthlyPayment = Principal / Months;
            }

            MonthlyPayment = Math.Round(MonthlyPayment, 2);

            _monthlyRateExceedsMonthlyPayment = MontlyInterestRateExceedsMontlyPayment();

            if (!_monthlyRateExceedsMonthlyPayment)
            {
                Double balance = Principal; // Local variable to keep track of the balance within the method.

                for (int iterations = 0; iterations < Months; iterations++)
                {
                    if (balance > MonthlyPayment)
                    {
                        balance = balance - MonthlyPayment;
                        balance = balance * (1.0 + MonthlyRate);
                        balance = Math.Round(balance, 2);
                    }
                    else
                    {
                        FinalPayment = balance * (1.0 + MonthlyRate);
                        FinalPayment = Math.Round(FinalPayment, 2);
                        balance = 0;
                    }
                }

                for (int iterations = 0; iterations < Months - 1; iterations++)
                {
                    Interest = Interest + MonthlyPayment;
                }

                Interest = Interest + FinalPayment;

                Interest = Interest - Principal;

                TotalLoanPayments = Principal + Interest;

                PaymentSchedule = CreatePaymentSchedule();
            }
        }

        /* This method ensures the monthly payment is sufficient to exceed the APR and
         * will pay off the loan.
         */
        private bool MontlyInterestRateExceedsMontlyPayment()
        {
            bool result;

            if ((Principal * MonthlyRate) > MonthlyPayment)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        // This method creates the payment schedule.
        public List<PaymentModel> CreatePaymentSchedule()
        {
            List<PaymentModel> paymentSchedule = new List<PaymentModel>();

            DateTime dueDate = FirstPaymentDueDate;

            for (int i = 0; i < Months - 1; i++)
            {
                PaymentModel payment = new PaymentModel(dueDate, MonthlyPayment);

                paymentSchedule.Add(payment);

                dueDate = dueDate.AddMonths(1);
            }

            PaymentModel lastPayment = new PaymentModel(dueDate, FinalPayment);

            LastPaymentDueDate = dueDate;

            paymentSchedule.Add(lastPayment);

            return paymentSchedule;
        }

        #endregion

        #region ISeriablizable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PaymentPlanName", this.PaymentPlanName);
            info.AddValue("Principal", this.Principal);
            info.AddValue("Interest", this.Interest);
            info.AddValue("TotalLoanPayments", this.TotalLoanPayments);
            info.AddValue("FirstPaymentDueDate", this.FirstPaymentDueDate);
            info.AddValue("LastPaymentDueDate", this.LastPaymentDueDate);
            info.AddValue("MonthlyPayment", this.MonthlyPayment);
            info.AddValue("FinalPayment", this.FinalPayment);
            info.AddValue("APR", this.APR);
            info.AddValue("MonthlyRate", this.MonthlyRate);
            info.AddValue("Months", this.Months);
            info.AddValue("MonthlyRateExceedsMonthlyPayment", this.MonthlyRateExceedsMonthlyPayment);
            info.AddValue("PaymentSchedule", this.PaymentSchedule);
        }

        #endregion

        #region IComparer Members

        public int Compare(PaymentPlan x, PaymentPlan y)
        {
            return string.Compare(x.PaymentPlanName, y.PaymentPlanName);
        }

        #endregion
    }
}