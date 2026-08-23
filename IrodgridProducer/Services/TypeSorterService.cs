
using IrodgridProducer.Models;

namespace IrodgridProducer.Services;

public class TypeSorterService
{
    public List<UAVAssetsLive> FilterToUav(List<AssetsLiveRaw> listOfObj)
    {   

        
        List<UAVAssetsLive> filteredList = new();

        foreach (var obj in listOfObj)
        {
            if (obj.Type == "UAV")
            {
                var newObj = new UAVAssetsLive
                {
                    AssetId = obj.AssetId,
                    Type = obj.Type,
                    RawValue = obj.RawValue, //need to parse to int 
                    LastUpdate = obj.LastUpdate
                };
                filteredList.Add(newObj);
            }
        }

        return filteredList;
    }

    public List<PerimeterAssetsLive> FilterToPerimeterSensor(List<AssetsLiveRaw> listOfObj)
    {


        List<PerimeterAssetsLive> filteredList = new();

        foreach (var obj in listOfObj)
        {
            if (obj.Type == "PerimeterSensor")
            {
                var newObj = new PerimeterAssetsLive
                {
                    AssetId = obj.AssetId,
                    Type = obj.Type,
                    RawValue = obj.RawValue,
                    LastUpdate = obj.LastUpdate
                };
                filteredList.Add(newObj);
            }
        }

        return filteredList;
    }





}