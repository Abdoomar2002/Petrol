using Petrol.Models;
using Petrol.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Petrol.Services
{
    public class PlaceService : Repository<Place>
    {
        public PlaceService() : base()
        {
        }
        public bool IsPlaceNameExists(string name)
        {
            var All = GetAll<Place>().ToList();
            return All.Any(e => e.Name == name);
        }
    }
}