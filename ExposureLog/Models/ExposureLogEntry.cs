﻿using System;

namespace ExposureLog.Models
{
    public class ExposureLogEntry
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int RiskRating { get; set; }
        public string Notes { get; set; }
    }
}
