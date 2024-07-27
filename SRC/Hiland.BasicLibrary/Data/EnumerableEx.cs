namespace Hiland.BasicLibrary.Data;

public static class EnumerableEx
{
    /// <summary>
    /// �����������ϵĵѿ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    /// <param name="firstCollection"></param>
    /// <param name="secondCollection"></param>
    /// <returns>Ԫ�鼯��</returns>
    public static IEnumerable<(T, P)> Product<T, P>(this IEnumerable<T> firstCollection, IEnumerable<P> secondCollection)
    {
        var result = firstCollection.SelectMany(s => secondCollection, (x, y) => (x, y));
        return result;
    }

     /// <summary>
     /// ��һ���ܣ��ۺ�Aggregate�ı�����
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
     /// ӳ�书�ܣ�SelectͶӰ�ı�����
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
     /// ���˹��ܣ�Where�ı�����
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="collection"></param>
     /// <param name="predicate">��������</param>
     /// <returns></returns>
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        return collection.Where(predicate);
    }
}
