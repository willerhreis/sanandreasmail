﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Cities of state
    /// </summary>
    public class City
    {
        /// <summary>
        /// Identifier of city
        /// </summary>
        public Guid CityId { get; set; }
        /// <summary>
        /// Name of city
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Abbreviation of city
        /// </summary>
        public string Abbreviation { get; set; }

        public override string ToString()
        {
            return "Id: " + this.CityId + "; Name: " + this.Name + " (" + this.Abbreviation + ")";
        }

    }


}
