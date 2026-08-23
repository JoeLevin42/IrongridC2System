
using IrodgridProducer.Models;

namespace IrodgridProducer.Services;

public class TypeSorterService
{
    public List<object> SortToType(List<AssetsLiveRaw> listOfObj ,string type)
    {   

        //this is generic gets as param the type and returns filterd list
        List<object> sortedList = new();

        foreach (var obj in listOfObj)
        {
            if (obj.AssetType == type)
            {
                sortedList.Add(obj);
            }
        }

        return sortedList;
    }
}