using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ScrumRetroApp.Data.Repositories
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		#region Fields
		protected readonly DbContext context;
		#endregion

		#region Constructors
		protected Repository(DbContext context)
		{
			this.context = context;
		}
		#endregion

		#region Publics
		/// <inheritdoc />
		public TEntity Get(int id)
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			return set.Any() ? set.Find(id) : null;
		}

		/// <inheritdoc />
		public TEntity Get(TEntity entity)
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			return set.Any() ? set.Find(entity) : null;
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> GetAll()
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			return set.Any() ? set.ToList() : null;
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			return set.Any() ? set.Where(predicate) : null;
		}

		/// <inheritdoc />
		public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			return set.Any() ? set.FirstOrDefault(predicate) : null;
		}

		/// <inheritdoc />
		public int Add(TEntity entity)
		{
			return Add(ToList(entity));
		}

		/// <inheritdoc />
		public int Add(IEnumerable<TEntity> entities)
		{
			context.Set<TEntity>().AddRange(entities);
			return context.SaveChanges();
		}

		/// <inheritdoc />
		public int Update(TEntity entity)
		{
			return Update(ToList(entity));
		}

		/// <inheritdoc />
		public int Update(IEnumerable<TEntity> entities)
		{
			context.Set<TEntity>().UpdateRange(entities);
			return context.SaveChanges();
		}

		/// <inheritdoc />
		public int Remove(TEntity entity)
		{
			return Remove(ToList(entity));
		}

		/// <inheritdoc />
		public int Remove(IEnumerable<TEntity> entities)
		{
			context.Set<TEntity>().RemoveRange(entities);
			return context.SaveChanges();
		}

		/// <inheritdoc />
		public int Remove(Expression<Func<TEntity, bool>> predicate)
		{
			DbSet<TEntity> set = context.Set<TEntity>();
			IQueryable<TEntity> list = set.Where(predicate);
			set.RemoveRange(list);
			return context.SaveChanges();
		}
		#endregion

		#region Privates
		private static IEnumerable<TEntity> ToList(TEntity entity)
		{
			return new List<TEntity> { entity };
		}
		#endregion
	}
}