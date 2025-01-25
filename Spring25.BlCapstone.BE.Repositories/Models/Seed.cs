﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Seed
    {
        [Key]
        public int Id { get; set; }
        public string SeedName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable {  get; set; }
        public double MinTemp {  get; set; }
        public double MaxTemp { get; set; }
        public double MinHumid {  get; set; }
        public double MaxHumid { get; set; }
        public double MinMoisture { get; set; }
        public double MaxMoisture { get; set; }
        public double MinFertilizer {  get; set; }
        public double MaxFertilizer { get;set; }
        public string FertilizerUnit {  get; set; }
        public double MinPesticide { get; set; }
        public double MaxPesticide { get; set; }
        public string PesticideUnit {  get; set; }
        public double MinBrixPoint {  get; set; }
        public double MaxBrixPoint { get;set; }
        public string GTTestKitColor {  get; set; }
    }
}
