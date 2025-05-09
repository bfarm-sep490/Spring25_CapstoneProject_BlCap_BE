﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class PlanModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("expert_id")]
        public int ExpertId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("season_name")]
        public string? SeasonName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("qr_code")]
        public string? QRCode { get; set; }
        [JsonPropertyName("seed_quantity")]
        public int? SeedQuantity { get; set; }
        [JsonPropertyName("evaluated_result")]
        public string? EvaluatedResult { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("is_approved")]
        public bool IsApproved { get; set; }
        [JsonPropertyName("contract_address")]
        public string UrlAddress { get; set; }
        [JsonPropertyName("order_information")]
        public List<OrderInfor>? OrderInfor { get; set; }
        [JsonPropertyName("plant_information")]
        public PlantInfor PlantInfor { get; set; }
        [JsonPropertyName("yield_information")]
        public YieldInfor? YieldInfor { get; set; }
        [JsonPropertyName("caring_task_information")]
        public List<PlanCaringInfor>? CaringTaskInfor { get; set; }
        [JsonPropertyName("inspecting_form_information")]
        public List<PlanInspectingInfor>? InspectingInfors { get; set; }
        [JsonPropertyName("harvesting_task_information")]
        public List<PlanHarvestingInfor>? HarvestingInfors { get; set; }
        [JsonPropertyName("packaging_task_information")]
        public List<PlanPackagingInfor>? PackagingInfors { get; set; }
        [JsonPropertyName("problem_information")]
        public List<ProblemInfor>? ProblemInfors { get; set; }
    }

    public class OrderInfor
    {
        [JsonPropertyName("order_id")]
        public int Id { get; set; }
        [JsonPropertyName("pre_order_quantity")]
        public float PreOrderQuantity { get; set; }
        [JsonPropertyName("order_plan_quantity")]
        public float OrderPlanQuantity { get; set; }
    }

    public class PlantInfor
    {
        [JsonPropertyName("plant_id")]
        public int Id { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class YieldInfor
    {
        [JsonPropertyName("yield_id")]
        public int Id { get; set; }
        [JsonPropertyName("yield_name")]
        public string YieldName { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("area_unit")]
        public string AreaUnit { get; set; }
        [JsonPropertyName("area")]
        public double Area { get; set; }
    }

    public class PlanCaringInfor
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("problem_id")]
        public int? ProblemId { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class PlanInspectingInfor
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("inspector_id")]
        public int? InspectorId { get; set; }
        [JsonPropertyName("task_name")]
        public string FormName { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class PlanHarvestingInfor
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("is_available")]
        public bool IsAvailable { get; set; }
    }

    public class PlanPackagingInfor
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class ProblemInfor
    {
        [JsonPropertyName("problem_id")]
        public int Id { get; set; }
        [JsonPropertyName("problem_name")]
        public string ProblemName { get; set; }
        [JsonPropertyName("date")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("problem_type")]
        public string ProblemType { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
