﻿using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IStepometerModelsService
    {
        Task<StepometerModel> GetData();
        void PostData(StepometerModel data);
        void PutData(StepometerModel data);
        void DeleteData(StepometerModel data);
    }
}
