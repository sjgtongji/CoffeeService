using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Data
{
    /// <summary>
    /// 实体上下文工厂，提供一组用于创建实体上下文的静态方法。
    /// EntityContextFactory 中的方法是提供给业务上下文使用的，数据层中不应直接调用这些方法（数据层应直接调用 ManagerBase 中定义的同名方法）。
    /// </summary>
    public class EntityContextFactory
    {
        /// <summary>
        /// 创建可同时执行读写操作的实体访问上下文对象。
        /// </summary>
        /// <returns></returns>
        public static IEntityContext CreateEntityContext()
        {
            return Container.EntityModelProvider.GetEntityContainer("Coffee").CreateEntityContext();
        }

        /// <summary>
        /// 创建可同时执行读写或者只读操作的实体访问上下文对象。
        /// </summary>
        /// <returns></returns>
        public static IEntityContext CreateEntityContext(bool readOnly)
        {
            return Container.EntityModelProvider.GetEntityContainer("Coffee").CreateEntityContext(readOnly);
        }

        /// <summary>
        /// 执行一个不需要返回结果的函数，该函数可接收一个已初始化的 IEntityContext 参数。
        /// 如果在执行过程中发生并发冲突，则会额外进行 concurrencyRetryCount 参数指定次数的重试，默认执行 1 次重试。
        /// </summary>
        /// <typeparam name="TResult">函数执行结果的类型。</typeparam>
        /// <param name="func">要执行的函数。</param>
        /// <param name="concurrencyRetryCount">并发重试次数。</param>
        /// <returns>函数执行的结果。</returns>
        public static TResult Execute<TResult>(Func<IEntityContext, TResult> func, int concurrencyRetryCount = 1)
        {
            int retryCount = 0;

            while (true)
            {
                using (IEntityContext entityContext = EntityContextFactory.CreateEntityContext())
                {
                    try
                    {
                        return func(entityContext);
                    }
                    catch (EntityConcurrencyException)
                    {
                        retryCount++;

                        if (retryCount > concurrencyRetryCount)
                        {
                            throw;
                        }

                        continue;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一个不需要返回结果的函数，该函数可接收一个已初始化的 IEntityContext 参数。
        /// 如果在执行过程中发生并发冲突，则会额外进行 concurrencyRetryCount 参数指定次数的重试，默认执行 1 次重试。
        /// </summary>
        /// <param name="action">要执行的函数。</param>
        /// <param name="concurrencyRetryCount">并发重试次数。</param>
        public static void Execute(Action<IEntityContext> action, int concurrencyRetryCount = 1)
        {
            int retryCount = 0;

            while (true)
            {
                using (IEntityContext entityContext = EntityContextFactory.CreateEntityContext())
                {
                    try
                    {
                        action(entityContext);

                        break;
                    }
                    catch (EntityConcurrencyException)
                    {
                        retryCount++;

                        if (retryCount > 3)
                        {
                            throw;
                        }

                        continue;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 在事物中执行一个需要返回结果的函数，该函数可接收一个已初始化的 IEntityContext 参数。
        /// 如果在执行过程中发生并发冲突，则会额外进行 concurrencyRetryCount 参数指定次数的重试，默认执行 1 次重试。
        /// </summary>
        /// <typeparam name="TResult">函数执行结果的类型。</typeparam>
        /// <param name="func">要在事物中执行的函数。</param>
        /// <param name="concurrencyRetryCount">并发重试次数。</param>
        /// <returns>函数执行的结果。</returns>
        public static TResult ExecuteInTransition<TResult>(Func<IEntityContext, TResult> func, int concurrencyRetryCount = 1)
        {
            int retryCount = 0;

            while (true)
            {
                using (IEntityContext entityContext = EntityContextFactory.CreateEntityContext())
                {
                    try
                    {
                        entityContext.BeginTransaction();

                        var result = func(entityContext);

                        entityContext.Commit();

                        return result;
                    }
                    catch (EntityConcurrencyException)
                    {
                        entityContext.Rollback();

                        retryCount++;

                        if (retryCount > concurrencyRetryCount)
                        {
                            throw;
                        }

                        continue;
                    }
                    catch
                    {
                        entityContext.Rollback();

                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 在事物中执行一个不需要返回结果的函数，该函数可接收一个已初始化的 IEntityContext 参数。
        /// 如果在执行过程中发生并发冲突，则会额外进行 concurrencyRetryCount 参数指定次数的重试，默认执行 1 次重试。
        /// </summary>
        /// <param name="action">要在事物中执行的函数。</param>
        /// <param name="concurrencyRetryCount">并发重试次数。</param>
        public static void ExecuteInTransition(Action<IEntityContext> action, int concurrencyRetryCount = 1)
        {
            int retryCount = 0;

            while (true)
            {
                using (IEntityContext entityContext = EntityContextFactory.CreateEntityContext())
                {
                    try
                    {
                        entityContext.BeginTransaction();

                        action(entityContext);

                        entityContext.Commit();

                        break;
                    }
                    catch (EntityConcurrencyException)
                    {
                        entityContext.Rollback();

                        retryCount++;

                        if (retryCount > concurrencyRetryCount)
                        {
                            throw;
                        }

                        continue;
                    }
                    catch
                    {
                        entityContext.Rollback();

                        throw;
                    }
                }
            }
        }
    }
}
