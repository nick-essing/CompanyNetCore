using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Model;

namespace CompanyNetCore.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetData();
    }
}
