using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsApplication6
{
    public class CustomerDataSources
    {
        public List<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();

            list.Add( new Customer{ ID=1, Name = "Charlie"});
            list.Add(new Customer { ID = 2, Name = "Tim" });
            list.Add(new Customer { ID = 3, Name = "Chris" });
            list.Add(new Customer { ID = 4, Name = "Arnie" });

            return list;
        }
    }
}
