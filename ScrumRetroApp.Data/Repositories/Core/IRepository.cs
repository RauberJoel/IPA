using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ScrumRetroApp.Data.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TEntity Get(int id);

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TEntity Get(TEntity entity);

		/// <summary>
		/// </summary>
		/// <returns></returns>
		IEnumerable<TEntity> GetAll();

		/// <summary>
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		int Add(TEntity entity);

		/// <summary>
		/// </summary>
		/// <param name="entities"></param>
		int Add(IEnumerable<TEntity> entities);

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		int Update(TEntity entity);

		/// <summary>
		/// </summary>
		/// <param name="entities"></param>
		int Update(IEnumerable<TEntity> entities);

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		int Remove(TEntity entity);

		/// <summary>
		/// </summary>
		/// <param name="entities"></param>
		int Remove(IEnumerable<TEntity> entities);

		/// <summary>
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		int Remove(Expression<Func<TEntity, bool>> predicate);
	}
}