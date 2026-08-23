
using IrodgridProducer.Models;

namespace IrodgridProducer.Services;

public class ValidateGenerator
{
    public List<UAVAssetsLive> UAVHandler(List<UAVAssetsLive> uavList)
    {

        List<UAVAssetsLive> fullUavList = new();
        foreach (var uav in uavList)
        {
            string generatedProcessedStatus = "Warning";
            bool generatedIsVerified = false;
            int checkVal;
            if (int.TryParse(uav.RawValue, out checkVal))
            {

                if (int.Parse(uav.RawValue) > 0 || int.Parse(uav.RawValue) < 20)
                {
                    generatedProcessedStatus = "Warning";
                    generatedIsVerified = true;
                }
                else if (int.Parse(uav.RawValue) > 20 || int.Parse(uav.RawValue) <= 100)
                {
                    generatedProcessedStatus = "Stable";
                    generatedIsVerified = true;
                }
            }

            var newFullObj = new UAVAssetsLive
            {
                AssetId = uav.AssetId,
                Type = uav.Type,
                RawValue = uav.RawValue,
                ProcessedStatus = generatedProcessedStatus,
                IsVerified = generatedIsVerified,
                LastUpdate = uav.LastUpdate,

            };
            fullUavList.Add(newFullObj);
        }

        return fullUavList;

    }



    public List<PerimeterAssetsLive> PermiterSensorHandler(List<PerimeterAssetsLive> perimeterSensorList)
    {
        List<PerimeterAssetsLive> fullPerimeterList = new();

        foreach (var per in perimeterSensorList)
        {
            string[] goodOptions =  { "Good", "GOOD", "good", "gud" };
            string[] badOptions =  { "Bad", "BAD", "bad", "bed" };
            string generatedProcessedStatus = "Warning";
            bool generatedIsVerified = false;

            if (goodOptions.Contains(per.RawValue))
            {
                generatedProcessedStatus = "Stable";
                generatedIsVerified = true;
            }
            else if (badOptions.Contains(per.RawValue))
            {
                generatedProcessedStatus = "Warning";
                generatedIsVerified = true;
            }


            var newFullObj = new PerimeterAssetsLive
            {
                AssetId = per.AssetId,
                Type = per.Type,
                RawValue = per.RawValue,
                ProcessedStatus = generatedProcessedStatus,
                IsVerified = generatedIsVerified,
                LastUpdate = per.LastUpdate,

            };
            fullPerimeterList.Add(newFullObj);
        }

        return fullPerimeterList;
    }
}