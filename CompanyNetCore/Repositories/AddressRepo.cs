using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using CompanyNetCore.Model;
using System.Linq;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;

namespace CompanyNetCore.Repositories
{
    class AddressRepo : IRepository<Address>
    {
        IDbContext _dbContext;
        public AddressRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Address> Read()
        {
                try
                {
                    List<Address> retVal;
                    var conn = _dbContext.GetCompany();
                    string Select = "SELECT Id,Postcode,City,Street,Country FROM viAddress;";
                    using (conn)
                    {
                        retVal = conn.Query<Address>(Select).ToList();
                    }
                    return retVal;
                }
                catch (Exception)
                {
                    throw new Helper.RepoException<ResultType>(ResultType.SQLERROR);
                }
        }
        public Address Read(int Id)
        {
            try
            {
                Address retVal;
                var conn = _dbContext.GetCompany();
                string Select = "SELECT Id,Postcode,City,Street,Country FROM viAddress WHERE Id = @Id;";
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    retVal = conn.QueryFirstOrDefault<Address>(Select, param);
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new Helper.RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Address Create(Address elm)
        {
            if (elm.Id != 0)
            {
                throw new Helper.RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retval = InsertOrUpdate(elm);
            return retval;
        }
        public Address Update(Address elm)
        {
            if (elm.Id == 0 || Read(elm.Id) == null)
            {
                throw new Helper.RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retval = InsertOrUpdate(elm);
            return retval;
        }
        private Address InsertOrUpdate(Address address)
        {
            try
            {
                Address retVal;
                var conn = _dbContext.GetCompany();
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", address.Id);
                    param.Add("@postcode", address.Postcode);
                    param.Add("@city", address.City);
                    param.Add("@street", address.Street);
                    param.Add("@country", address.Country);
                    retVal = conn.QueryFirstOrDefault<Address>("spInsertOrUpdateAddress", param, null, null, CommandType.StoredProcedure);
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new Helper.RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Address Delete(int Id)
        {
            try
            {
                Address retVal;
                var conn = _dbContext.GetCompany();
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    retVal = conn.QueryFirstOrDefault<Address>("spDeleteAddress", param, null, null, CommandType.StoredProcedure);
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new Helper.RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
    }
}
