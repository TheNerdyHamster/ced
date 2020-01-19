﻿using Api.Data;
using Api.Domain.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ComponentService : IComponentService
    {

        private readonly DataContext _datacontext;
        public ComponentService(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task<Tuple<bool, int>> CreateComponents(List<Component> components)
        {
            _datacontext.Components.AddRange(components);
            var created = await _datacontext.SaveChangesAsync();
            return new Tuple<bool, int>(created > 0, created);
        }
    }
}
