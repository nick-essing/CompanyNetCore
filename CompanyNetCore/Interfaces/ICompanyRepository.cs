using FirmaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaTest.Interfaces
{
    public interface ICompanyRepository
    {
        List<CompanyData> GetNames();
    }
}
