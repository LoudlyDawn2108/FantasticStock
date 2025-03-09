using FantasticStock.Common;
using FantasticStock.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.ViewModels.Sales
{
    class CustomerManagementViewModel : ViewModelBase
    {
        public IEnumerable<Customer> Customers { get; internal set; }

        internal Customer AddCustomer(Customer selectedCustomer)
        {
            throw new NotImplementedException();
        }

        internal bool DeactivateCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        internal Customer GetCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<Customer> SearchCustomers(string searchText)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateCustomer(Customer selectedCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
