﻿namespace StockData.Infrastructure.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
