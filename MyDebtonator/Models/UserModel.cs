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
    public class UserModel : ObservableObject
    {
        #region Fields

        private string _userName;
        private Double _monthlyNetIncome;
        private Double _totalPrincipal;
        private Double _totalInterest;
        private Double _totalPayments;
        private DateTime _earliestPaymentDueDate;
        private DateTime _latestPaymentDueDate;
        private Double _totalPaidPerMonth;
        private Double _averageMonthlyPayment;
        private Double _debtPercentage;
        private List<PaymentPlanModel> _plans;

        #endregion

        #region Properties

        public string Password { get; set; }

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public Double MonthlyNetIncome
        {
            get
            {
                return this._monthlyNetIncome;
            }
            set
            {
                this._monthlyNetIncome = value;
                OnPropertyChanged("MonthlyNetIncome");
            }
        }

        public Double TotalPrincipal
        {
            get
            {
                return this._totalPrincipal;
            }
            set
            {
                this._totalPrincipal = value;
                OnPropertyChanged("TotalPrincipal");
            }
        }

        public Double TotalInterest
        {
            get
            {
                return this._totalInterest;
            }
            set
            {
                this._totalInterest = value;
                OnPropertyChanged("TotalInterest");
            }
        }

        public Double TotalPayments
        {
            get
            {
                return this._totalPayments;
            }
            set
            {
                this._totalPayments = value;
                OnPropertyChanged("TotalPayments");
            }
        }

        public DateTime EarliestPaymentDueDate
        {
            get
            {
                return this._earliestPaymentDueDate;
            }
            set
            {
                this._earliestPaymentDueDate = value;
                OnPropertyChanged("EarliestPaymentDueDate");
            }
        }

        public DateTime LatestPaymentDueDate
        {
            get
            {
                return this._latestPaymentDueDate;
            }
            set
            {
                this._latestPaymentDueDate = value;
                OnPropertyChanged("LatestPaymentDueDate");
            }
        }

        public Double TotalPaidPerMonth
        {
            get
            {
                return this._totalPaidPerMonth;
            }
            set
            {
                this._totalPaidPerMonth = value;
                OnPropertyChanged("TotalPaidPerMonth");
            }
        }

        public Double AverageMonthlyPayment
        {
            get
            {
                return this._averageMonthlyPayment;
            }
            set
            {
                this._averageMonthlyPayment = value;
                OnPropertyChanged("AverageMonthlyPayment");
            }
        }

        public Double DebtPercentage
        {
            get
            {
                return this._debtPercentage;
            }
            set
            {
                this._debtPercentage = value;
                OnPropertyChanged("DebtPercentage");
            }
        }

        public List<PaymentPlanModel> Plans
        {
            get
            {
                return this._plans;
            }
            set
            {
                this._plans = value;
                OnPropertyChanged("Plans");
            }
        }

        #endregion

        #region Constructors

        public UserModel()
        {
            UserName = string.Empty;
            Password = string.Empty;

            MonthlyNetIncome = 0.0;
            TotalInterest = 0.0;
            TotalPayments = 0.0;

            EarliestPaymentDueDate = new DateTime(2000, 1, 1);
            LatestPaymentDueDate = new DateTime(2099, 12, 31);
            TotalPaidPerMonth = 0.0;
            AverageMonthlyPayment = 0.0;

            DebtPercentage = 0.0;

            Plans = new List<PaymentPlanModel>();
        }

        public UserModel(string name, string password, Double income) : this()
        {
            UserName = name;
            Password = password;

            MonthlyNetIncome = income;
        }

        #endregion

        #region Methods

        // Adds a plan.
        public void AddPlan(PaymentPlanModel plan)
        {
            Plans.Add(plan);

            UpdateUser();
        }

        // Deletes a plan.
        public void DeletePlan(PaymentPlanModel plan)
        {
            Plans.Remove(plan);

            UpdateUser();
        }

        // Updates a user.
        public void UpdateUser()
        {
            TotalPrincipal = 0.0;
            TotalInterest = 0.0;
            TotalPayments = 0.0;
            EarliestPaymentDueDate = DateTime.MinValue;
            LatestPaymentDueDate = DateTime.MinValue;
            TotalPaidPerMonth = 0.0;
            AverageMonthlyPayment = 0.0;
            DebtPercentage = 0.0;

            if (Plans.Count > 0)
            {
                foreach (PaymentPlanModel p in Plans)
                {
                    TotalPrincipal = TotalPrincipal + p.Principal;
                    TotalInterest = TotalInterest + p.Interest;
                    TotalPayments = TotalPayments + p.Principal + p.Interest;

                    if (EarliestPaymentDueDate == DateTime.MinValue)
                    {
                        EarliestPaymentDueDate = p.FirstPaymentDueDate;
                    }
                    else
                    {
                        if (p.FirstPaymentDueDate < EarliestPaymentDueDate)
                        {
                            EarliestPaymentDueDate = p.FirstPaymentDueDate;
                        }
                    }

                    TotalPaidPerMonth = TotalPaidPerMonth + p.MonthlyPayment;
                }
            }

            AverageMonthlyPayment = TotalPaidPerMonth / Plans.Count;

            AverageMonthlyPayment = Math.Round(AverageMonthlyPayment, 2);

            DebtPercentage = Math.Round((TotalPaidPerMonth / MonthlyNetIncome), 4);
        }

        // Update the properties of a user based on new entries
        public void UpdateUsersState(string name, string password, Double netMonthlyIncome)
        {
            this.UserName = name;
            this.Password = password;
            this.MonthlyNetIncome = netMonthlyIncome;
            this.DebtPercentage = TotalPaidPerMonth / MonthlyNetIncome;
        }

        #endregion
    }
}
