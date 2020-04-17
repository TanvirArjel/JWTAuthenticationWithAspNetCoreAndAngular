﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JwtAuthentication.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entities { get; set; }

        /// <summary>
        /// This method takes <paramref name="specification"/> as parameter and returns <see cref="List{T}"/> where T is <see cref="TEntity"/>.
        /// </summary>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <returns><see cref="List{TEntity}"/></returns>
        Task<List<TEntity>> GetEntityListAsync(Specification<TEntity> specification);

        /// <summary>
        /// This method takes <paramref name="specification"/> and <paramref name="selectExpression"/> as parameters and
        /// returns <see cref="List{T}"/> where T is <see cref="TProjectedType"/>.
        /// </summary>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="selectExpression">The projection data will be projected, aka <c>LINQ</c> select query.</param>
        /// <returns><see cref="List{TProjectedType}"/> where T is <see cref="TProjectedType"/></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="selectExpression"/> is null.</exception>
        Task<List<TProjectedType>> GetEntityListAsync<TProjectedType>(
            Specification<TEntity> specification,
            Expression<Func<TEntity, TProjectedType>> selectExpression)
            where TProjectedType : class;

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <code>null</code>.
        /// </summary>
        /// <param name="id">The primary key value of the entity.</param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> GetEntityByIdAsync(object id);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the specified projected entity
        /// if found otherwise null.
        /// </summary>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="selectExpression">The <code>LINQ</code> select query.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is null.</exception>
        Task<TProjectedType> GetEntityByIdAsync<TProjectedType>(object id, Expression<Func<TEntity, TProjectedType>> selectExpression);

        /// <summary>
        /// This method takes <paramref name="specification"/> as parameter and returns <see cref="TEntity"/>.
        /// </summary>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> GetEntityAsync(Specification<TEntity> specification);

        /// <summary>
        /// This method takes <paramref name="specification"/> and <paramref name="selectExpression"/> and returns <see cref="TProjectedType"/>.
        /// </summary>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="selectExpression">The select <code>LINQ</code> query.</param>
        /// <returns><see cref="TProjectedType"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="selectExpression"/> is <code>null</code>.</exception>
        Task<TProjectedType> GetEntityAsync<TProjectedType>(Specification<TEntity> specification, Expression<Func<TEntity,
            TProjectedType>> selectExpression);

        /// <summary>
        /// This method takes the <paramref name="condition"/> based on which existence of the entity will be checked
        /// and returns <see cref="Task"/> of <see cref="Boolean"/>.
        /// </summary>
        /// <param name="condition">The condition based on which the existence will checked.</param>
        /// <returns></returns>
        Task<bool> IsEntityExistsAsync(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// This method takes <see cref="TEntity"/> to be inserted and returns <see cref="Task"/>. Insertion in the database will be done after
        /// calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        Task InsertEntityAsync(TEntity entity);

        /// <summary>
        /// This method takes <code>IEnumerable</code> of entities to be inserted and returns <see cref="Task"/>.
        /// Insertion in the database will be done after calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entities">The entities to be inserted.</param>
        /// <returns>Returns <see cref="Task"/></returns>
        Task InsertEntitiesAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method takes <see cref="TEntity"/> to be updated and returns <code>void</code>. Update in the database will be done after
        /// calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void UpdateEntity(TEntity entity);

        /// <summary>
        /// This method takes <code>IEnumerable</code> of entities to be updated and returns <code>void</code>. Update in the database will be done after
        /// calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entities">The entities to be updated.</param>
        void UpdateEntities(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method takes <see cref="TEntity"/>, delete the entity and returns <code>void</code>. Deletion in the database will be done after
        /// calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void DeleteEntity(TEntity entity);

        /// <summary>
        /// This method takes <code>IEnumerable</code> of entities, delete those entities and returns <code>void</code>. Deletion in the database will
        /// be done after calling <code>IUnitOfWork.SaveChangesAsync()</code> method.
        /// </summary>
        /// <param name="entities">The list of entities to be deleted.</param>
        void DeleteEntities(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method takes one or more <paramref name="conditions"/> and returns the count in <see cref="int"/> type.
        /// </summary>
        /// <param name="conditions">The condition or conditions based on which count will be done.</param>
        /// <returns><see cref="int"/></returns>
        Task<int> GetCountAsync(params Expression<Func<TEntity, bool>>[] conditions);

        /// <summary>
        /// This method takes one or more <paramref name="conditions"/> and returns the count in <see cref="long"/> type.
        /// </summary>
        /// <param name="conditions">The condition or conditions based on which count will be done.</param>
        /// <returns><see cref="long"/></returns>
        Task<long> GetLongCountAsync(params Expression<Func<TEntity, bool>>[] conditions);
    }
}
