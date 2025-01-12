﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace readShorts.Models.Interfaces
{

    /// <summary>
    /// IRepository definition.
    /// 
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    public interface IRepository<T> : IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable where T : IEntity
    {
        /// <summary>
        /// Returns the T by its given id.
        /// 
        /// </summary>
        /// <param name="id">The string representing the ObjectId of the entity to retrieve.</param>
        /// <returns>
        /// The Entity T.
        /// </returns>
        T GetById(string id);
        /// <summary>
        /// Returns a single T by the given criteria.
        /// 
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>
        /// A single T matching the criteria.
        /// </returns>
        T GetSingle(Expression<Func<T, bool>> criteria);
        /// <summary>
        /// Returns All the records of T.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// IQueryable of T.
        /// </returns>
        [Obsolete("The repository itself now implements IQueryable<T>")]
        IQueryable<T> All();
        /// <summary>
        /// Returns the list of T where it matches the criteria.
        /// 
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>
        /// IQueryable of T.
        /// </returns>
        [Obsolete("The repository itself now implements IQueryable<T>")]
        IQueryable<T> All(Expression<Func<T, bool>> criteria);
        /// <summary>
        /// Adds the new entity in the repository.
        /// 
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The added entity including its new ObjectId.
        /// </returns>
        T Add(T entity);
        /// <summary>
        /// Adds the new entities in the repository.
        /// 
        /// </summary>
        /// <param name="entities">The entities of type T.</param>
        void Add(IEnumerable<T> entities);
        /// <summary>
        /// Upserts an entity.
        /// 
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The updated entity.
        /// </returns>
        T Update(T entity);
        /// <summary>
        /// Upserts the entities.
        /// 
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        void Update(IEnumerable<T> entities);
        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// 
        /// </summary>
        /// <param name="id">The string representation of the entity's id.</param>
        void Delete(string id);
        /// <summary>
        /// Deletes the given entity.
        /// 
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
        /// <summary>
        /// Deletes the entities matching the criteria.
        /// 
        /// </summary>
        /// <param name="criteria">The expression.</param>
        void Delete(Expression<Func<T, bool>> criteria);
        /// <summary>
        /// Deletes all entities in the repository.
        /// 
        /// </summary>
        void DeleteAll();
        /// <summary>
        /// Counts the total entities in the repository.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Count of entities in the repository.
        /// </returns>
        long Count();
        /// <summary>
        /// Checks if the entity exists for given criteria.
        /// 
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>
        /// true when an entity matching the criteria exists, false otherwise.
        /// </returns>
        bool Exists(Expression<Func<T, bool>> criteria);
        /// <summary>
        /// Lets the server know that this thread is about to begin a series of related operations that must all occur
        ///             on the same connection. The return value of this method implements IDisposable and can be placed in a using
        ///             statement (in which case RequestDone will be called automatically when leaving the using statement).
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A helper object that implements IDisposable and calls RequestDone() from the Dispose method.
        /// </returns>
        /// 
        /// <remarks>
        /// Sometimes a series of operations needs to be performed on the same connection in order to guarantee correct
        ///             results. This is rarely the case, and most of the time there is no need to call RequestStart/RequestDone.
        ///             An example of when this might be necessary is when a series of Inserts are called in rapid succession with
        ///             SafeMode off, and you want to query that data in a consistent manner immediately thereafter (with SafeMode
        ///             off the writes can queue up at the server and might not be immediately visible to other connections). Using
        ///             RequestStart you can force a query to be on the same connection as the writes, so the query won't execute
        ///             until the server has caught up with the writes.
        ///             A thread can temporarily reserve a connection from the connection pool by using RequestStart and
        ///             RequestDone. You are free to use any other databases as well during the request. RequestStart increments a
        ///             counter (for this thread) and RequestDone decrements the counter. The connection that was reserved is not
        ///             actually returned to the connection pool until the count reaches zero again. This means that calls to
        ///             RequestStart/RequestDone can be nested and the right thing will happen.
        /// 
        /// </remarks>
        IDisposable RequestStart();
        /// <summary>
        /// Lets the server know that this thread is about to begin a series of related operations that must all occur
        ///             on the same connection. The return value of this method implements IDisposable and can be placed in a using
        ///             statement (in which case RequestDone will be called automatically when leaving the using statement).
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A helper object that implements IDisposable and calls RequestDone() from the Dispose method.
        /// </returns>
        /// <param name="slaveOk">Whether queries should be sent to secondary servers.</param>
        /// <remarks>
        /// Sometimes a series of operations needs to be performed on the same connection in order to guarantee correct
        ///             results. This is rarely the case, and most of the time there is no need to call RequestStart/RequestDone.
        ///             An example of when this might be necessary is when a series of Inserts are called in rapid succession with
        ///             SafeMode off, and you want to query that data in a consistent manner immediately thereafter (with SafeMode
        ///             off the writes can queue up at the server and might not be immediately visible to other connections). Using
        ///             RequestStart you can force a query to be on the same connection as the writes, so the query won't execute
        ///             until the server has caught up with the writes.
        ///             A thread can temporarily reserve a connection from the connection pool by using RequestStart and
        ///             RequestDone. You are free to use any other databases as well during the request. RequestStart increments a
        ///             counter (for this thread) and RequestDone decrements the counter. The connection that was reserved is not
        ///             actually returned to the connection pool until the count reaches zero again. This means that calls to
        ///             RequestStart/RequestDone can be nested and the right thing will happen.
        /// 
        /// </remarks>
        IDisposable RequestStart(bool slaveOk);
        /// <summary>
        /// Lets the server know that this thread is done with a series of related operations.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Instead of calling this method it is better to put the return value of RequestStart in a using statement.
        /// 
        /// </remarks>
        void RequestDone();
    }
}
