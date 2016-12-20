using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YYT.Model;
using System.Linq.Expressions;
using EntityFramework.Extensions;

namespace YYT.DAL
{
    public class BaseDAO<T> where T : class
    {
        public DbContext context;
        public BaseDAO()
        {
            this.context = new YYT_DBEntities();
        }

        public  int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public int AddRange(List<T> entityList)
        {
            context.Set<T>().AddRange(entityList);
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            var set = context.Set<T>();
            set.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int UpdateByWhere(Expression<Func<T, bool>> conditions, Expression<Func<T, T>> updateExpression)
        {
            return context.Set<T>().Where(conditions).Update(updateExpression);
        }

        public int Delete(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Deleted;
            return context.SaveChanges();
        }

        public int DeleteByWhere(Expression<Func<T, bool>> conditions)
        {
            return context.Set<T>().Where(conditions).Delete();
        }

        public T Find(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);
        }
     
        public T GetSingle<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction)
        {
            direction = String.IsNullOrWhiteSpace(direction) ? "desc" : direction;
            var result = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;
            if (orderBy != null)
                result = direction.ToLower() == "asc" ? result.OrderBy(orderBy) : result.OrderByDescending(orderBy);

            return result.FirstOrDefault();
        }

        public List<T> FindAll<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction)
        {
            direction = String.IsNullOrWhiteSpace(direction) ? "desc" : direction;
            var result = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;
            if (orderBy != null)
                result = direction.ToLower() == "asc" ? result.OrderBy(orderBy) : result.OrderByDescending(orderBy);
            return result.ToList();
        }

        public List<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
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
