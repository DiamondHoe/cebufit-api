﻿using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using System.Xml.Linq;

namespace CebuFitApi.Repositories
{
    public class MealRepository : IMealRepository
    {
        public Task CreateAsync(Meal blogPost)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Meal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Meal> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Meal blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
