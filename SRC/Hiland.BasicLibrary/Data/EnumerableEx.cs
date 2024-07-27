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


}
