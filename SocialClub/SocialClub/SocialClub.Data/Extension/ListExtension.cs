using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SocialClub.Data.Extension
{
    public static class ListExtension
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            Double d = 1.0D;
            d.ToString();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);                
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataRow ToDataRow<T>(this T data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            DataRow row = table.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(data) ?? DBNull.Value;
            }

            table.Rows.Add(row);

            return table.Rows[0];
        }
    }
}
