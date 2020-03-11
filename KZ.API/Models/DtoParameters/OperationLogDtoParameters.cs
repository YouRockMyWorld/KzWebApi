﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.DtoParameters
{
    public class OperationLogDtoParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 20;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; } = DateTime.Now;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value >= MaxPageSize ? MaxPageSize : value;
        }
    }
}
