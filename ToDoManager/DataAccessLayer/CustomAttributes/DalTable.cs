using System;
using Shared.Enums;

namespace DataAccessLayer.CustomAttributes
{
    public class DalTable : Attribute
    {
        public DalTable(ConnectionStrings connectionString, string tableName, string identityColumnName)
        {
            ConnectionString = connectionString;
            TableName = tableName;
            IdentityColumnName = identityColumnName;
        }

        public ConnectionStrings ConnectionString { get; }
        public string TableName { get; }
        public string IdentityColumnName { get; }
    }
}