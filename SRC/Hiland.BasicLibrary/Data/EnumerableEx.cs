namespace Hiland.BasicLibrary.Data;

public static class EnumerableEx
{
    /// <summary>
    /// 计算两个集合的笛卡尔积
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    /// <param name="firstCollection"></param>
    /// <param name="secondCollection"></param>
    /// <returns>元组集合</returns>
    public static IEnumerable<(T, P)> Product<T, P>(this IEnumerable<T> firstCollection, IEnumerable<P> secondCollection)
    {
        var result = firstCollection.SelectMany(s => secondCollection, (x, y) => (x, y));
        return result;
    }

     /// <summary>
     /// 归一功能（聚合Aggregate的别名）
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="collection"></param>
     /// <param name="func"></param>
     /// <returns></returns>
    public static T Reduce<T>(this IEnumerable<T> collection, Func<T, T, T> func)
    {
        var result = collection.Aggregate(func);
        return result;
    }

     /// <summary>
     /// 映射功能（Select投影的别名）
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <typeparam name="P"></typeparam>
     /// <param name="collection"></param>
     /// <param name="func"></param>
     /// <returns></returns>
    public static IEnumerable<P> Map<T, P>(this IEnumerable<T> collection, Func<T, P> func)
    {
        return collection.Select(func);
    }

     /// <summary>
     /// 过滤功能（Where的别名）
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="collection"></param>
     /// <param name="predicate">过滤条件</param>
     /// <returns></returns>
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        return collection.Where(predicate);
    }
}
