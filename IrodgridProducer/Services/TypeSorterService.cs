
using IrodgridProducer.Models;

namespace IrodgridProducer.Services;

public class TypeSorterService
{
    public List<UAVAssetsLive> FilterToUav(List<AssetsLiveRaw> listOfObj)
    {   

        
        List<UAVAssetsLive> result = new();
        
        if (listOfObj == null)
        {
            return result;
        }
        foreach (var obj in listOfObj)
        {
            if (obj.AssetType == "UAV")
            {
                var newObj = new UAVAssetsLive
                {
                    AssetId = obj.AssetId,
                    AssetType = obj.AssetType,
                    RawValue = obj.RawValue, //need to parse to int 
                    LastUpdate = obj.LastUpdate
                };
                result.Add(newObj);
            }
        }

        return result;
    }
        
    public List<PerimeterAssetsLive> FilterToPerimeterSensor(List<AssetsLiveRaw> listOfObj)
    {


        List<PerimeterAssetsLive> result = new();
      

        if (listOfObj == null)
        {
            return result;
        }

        foreach (var obj in listOfObj)
        {
            if (obj.AssetType == "PerimeterSensor")
            {
                var newObj = new PerimeterAssetsLive
                {
                    AssetId = obj.AssetId,
                    AssetType = obj.AssetType,
                    RawValue = obj.RawValue,
                    LastUpdate = obj.LastUpdate
                };
                result.Add(newObj);
            }
        }

        return result;
    }





}