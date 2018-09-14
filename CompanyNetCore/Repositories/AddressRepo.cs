using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using CompanyNetCore.Model;
using System.Linq;

namespace CompanyNetCore.Repositories
{
    class AddressRepo
    {
        static AddressRepo _addressRepo;
        public static AddressRepo GetInstance()
        {
            if (_addressRepo == null)
                _addressRepo = new AddressRepo();
            return _addressRepo;
        }
        private AddressRepo()
        {

        }
        public List<Address> Read()
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                conn.Open();
                var result = conn.Query<Address>("SELECT Id,Postcode,City,Street,Country FROM viAddress").ToList();
                return result;
            }

        }
        public Address Read(int Id)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var result = conn.QueryFirstOrDefault<Address>("SELECT Id,Postcode,City,Street,Country FROM viAddress where Id = @Id", param);
                return result;
            }
        }
        public Address Create(Address address)
        {
            var retval = InsertOrUpdate(address);
            return retval;
        }
        public Address Update(Address address)
        {
            var retval = InsertOrUpdate(address);
            return retval;
        }
        private Address InsertOrUpdate(Address address)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", address.Id);
                param.Add("@postcode", address.Postcode);
                param.Add("@city", address.City);
                param.Add("@street", address.Street);
                param.Add("@country", address.Country);
                var result = conn.QueryFirstOrDefault<Address>("spInsertOrUpdateAddress", param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
        public Address spDelete( int Id)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var result = conn.QueryFirstOrDefault<Address>("spDeleteAddress", param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
