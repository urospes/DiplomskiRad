﻿using APIGateway.DataClasses;

namespace APIGateway.IServices
{
    public interface IGatewayService
    {
        public Task<List<CarDTO>> GetCars();
    }
}