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
        IMessageHelper _messageHelper;
            public AddressRepo(IDbContext dbContext, IMessageHelper messageHelper)
        {
            _dbContext = dbContext;
            _messageHelper = messageHelper;
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
                throw new RepoException<ResultType>(ResultType.SQLERROR);
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
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Address Create(Address elm)
        {
            if (elm.Id != 0)
            {
                throw new RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retVal = InsertOrUpdate(elm);
            _messageHelper.SendIntercom($"Addresse Hinzugefügt: {retVal.City}, {retVal.Street}");
            return retVal;
        }
        public Address Update(Address elm)
        {
            if (elm.Id == 0 || Read(elm.Id) == null)
            {
                throw new RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retVal = InsertOrUpdate(elm);
            _messageHelper.SendIntercom($"Addresse Geändert: {retVal.City}, {retVal.Street}");
            return retVal;
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
                throw new RepoException<ResultType>(ResultType.SQLERROR);
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
                _messageHelper.SendIntercom($"Addresse Gelöscht: {retVal.City}, {retVal.Street}");
                return retVal;
            }
            catch (Exception)
            {
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
    }
}
