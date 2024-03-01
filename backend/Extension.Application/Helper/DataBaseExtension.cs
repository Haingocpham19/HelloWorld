using Extension.Application.Dto.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;

namespace Extension.Application.Helper
{
    public static class OrdDataBaseUtil
    {
        public static async Task<PagedResultDto<T>> GetPagedAsync<T>(this IQueryable<T> query, PagedFullInputDto input)
        {
            var result = new PagedResultDto<T>
            {
                TotalCount = await query.CountAsync()
            };

            if (result.TotalCount == 0) return result;
            if (!string.IsNullOrEmpty(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }
            if (input.IsFullRecord.HasValue && input.IsFullRecord.Value)
            {
                result.Items = await query.ToListAsync();
                return result;
            }
            result.Items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return result;
        }

        //public static async Task<PagedResultDto<T>> GetPagedToFileExportAsync<T>(this IQueryable<T> query, PagedFullInputDto input)
        //{
        //    var result = new PagedResultDto<T>();
        //    if (!string.IsNullOrEmpty(input.Sorting))
        //    {
        //        query = query.OrderBy(input.Sorting);
        //    }

        //    if (input.IsFullRecord.HasValue && input.IsFullRecord.Value)
        //    {
        //        result.Items = await query.ToListAsync();
        //        return result;
        //    }
        //    result.Items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
        //    return result;
        //}

        //public static async Task<PagedResultDto<T>> GetPagedWithDapperAsync<T>(this IUnitOfWorkDapper dapper, string query, object input, int skipCount, int maxResultCount, string sorting)
        //{
        //    var result = new PagedResultDto<T>();

        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return result;
        //    }

        //    result.TotalCount =
        //        await dapper.QueryFirstOrDefaultAsync<int>($"select count(1) totalCount from ({query}) A ", input);
        //    if (result.TotalCount == 0) return result;

        //    string queryPaging = $"select * from ({query}) A {sorting} limit {skipCount}, {maxResultCount}";

        //    result.Items =
        //        (await dapper.QueryAsync<T>(queryPaging, input)).ToList();
        //    return result;
        //}


        //public static async Task<PagedResultDto<T>> GetPagedCustomWithDapperAsync<T>(this IUnitOfWorkDapper dapper, string query, object input, int skipCount, int maxResultCount, string sorting)
        //{
        //    var result = new PagedResultDto<T>();

        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return result;
        //    }
        //    using (var multi = await dapper.Connection.QueryMultipleAsync(query))
        //    {
        //        result.TotalCount = multi.Read<int>().Single();
        //        result.Items = multi.Read<T>().ToList();
        //    }

        //    return result;
        //}

        //public static async Task<PagedResultDto<object>> GetPagedWithRawSqlAsync(this IUnitOfWorkDapper dapper, string query, PagedFullInputDto input, object paramater)
        //{
        //    var result = new PagedResultDto<object>();

        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return result;
        //    }
        //    result.TotalCount =
        //        await dapper.QueryFirstOrDefaultAsync<int>($"select count(1) totalCount from ({query}) A ", paramater);
        //    if (result.TotalCount == 0) return result;
        //    result.Items =
        //        (await dapper.QueryAsync<object>($"select * from ({query}) A limit {input.SkipCount}, {input.MaxResultCount}", paramater)).ToList();
        //    return result;
        //}

        //public static DynamicParameters ConvertObjectToDynamicParameters(this object prm)
        //{
        //    var parameters = new DynamicParameters();
        //    if (prm != null)
        //    {
        //        foreach (var x in (JObject)prm)
        //        {
        //            string name = x.Key;
        //            var value = x.Value.ToObject<object>();
        //            parameters.Add(name, value);
        //        }
        //    }

        //    return parameters;
        //}

        //public static DataSet ToDataSet<T>(this IList<T> list, string tableName)
        //{
        //    Type elementType = typeof(T);
        //    DataSet ds = new DataSet();
        //    DataTable t = new DataTable(tableName);
        //    ds.Tables.Add(t);

        //    //add a column to table for each public property on T
        //    foreach (var propInfo in elementType.GetProperties())
        //    {
        //        Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

        //        t.Columns.Add(propInfo.Name, ColType);
        //    }

        //    //go through each property on T and add each value to the table
        //    foreach (T item in list)
        //    {
        //        DataRow row = t.NewRow();

        //        foreach (var propInfo in elementType.GetProperties())
        //        {
        //            row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
        //        }

        //        t.Rows.Add(row);
        //    }

        //    return ds;
        //}
    }

}
