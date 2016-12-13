using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YYT.Model;
using System.Linq.Expressions;
using EntityFramework.Extensions;

namespace YYT.DAL
{
    public class BaseDAO
    {
        public DbContext context;
        public BaseDAO()
        {
            this.context = new YYT_DBEntities();
        }

        public int Add<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public int Update<T>(T entity) where T : class
        {
            var set = context.Set<T>();
            set.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int UpdateByWhere<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, T>> updateExpression) where T : class
        {
            return context.Set<T>().Where(conditions).Update(updateExpression);
        }

        public int Delete<T>(T entity) where T : class
        {
            context.Entry<T>(entity).State = EntityState.Deleted;
            return context.SaveChanges();
        }

        public int DeleteByWhere<T>(Expression<Func<T, bool>> conditions) where T : class
        {
            return context.Set<T>().Where(conditions).Delete();
        }

        public T Find<T>(params object[] keyValues) where T : class
        {
            return context.Set<T>().Find(keyValues);
        }
     
        public T GetSingle<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction) where T : class
        {
            direction = String.IsNullOrWhiteSpace(direction) ? "desc" : direction;
            var result = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;
            if (orderBy != null)
                result = direction.ToLower() == "asc" ? result.OrderBy(orderBy) : result.OrderByDescending(orderBy);

            return result.FirstOrDefault();
        }

        public List<T> FindAll<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction) where T : class
        {
            direction = String.IsNullOrWhiteSpace(direction) ? "desc" : direction;
            var result = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;
            if (orderBy != null)
                result = direction.ToLower() == "asc" ? result.OrderBy(orderBy) : result.OrderByDescending(orderBy);
            return result.ToList();
        }

        public List<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord) where T : class
        {
            direction = String.IsNullOrWhiteSpace(direction) ? "desc" : direction;
            var result = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;
            if (orderBy != null)
                result = direction.ToLower() == "asc" ? result.OrderBy(orderBy) : result.OrderByDescending(orderBy);

            totalRecord = result.Count();
            if (totalRecord <= 0) return new List<T>();

            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
        }
    }
}
