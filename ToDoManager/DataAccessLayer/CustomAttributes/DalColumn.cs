using System;

namespace DataAccessLayer.CustomAttributes
{
    public class DalColumn : Attribute
    {
        public DalColumn(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; }
    }
}