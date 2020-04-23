﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class AppModelContainer : DbContext
    {
        public AppModelContainer()
            : base("name=AppModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BEPData> BEPDatas { get; set; }
        public DbSet<BERDataItemWiseQuantity> BERDataItemWiseQuantities { get; set; }
        public DbSet<BERDataPeopleWiseQuantity> BERDataPeopleWiseQuantities { get; set; }
        public DbSet<DetailData> DetailDatas { get; set; }
        public DbSet<DistrictByUserProfile> DistrictByUserProfiles { get; set; }
        public DbSet<DistrictData> DistrictDatas { get; set; }
        public DbSet<DistrictQuestion> DistrictQuestions { get; set; }
        public DbSet<DistrictSummery> DistrictSummeries { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<HnppData> HnppDatas { get; set; }
        public DbSet<ProgramByUserProfile> ProgramByUserProfiles { get; set; }
        public DbSet<RoleDefaultPage> RoleDefaultPages { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StandingDataPcRelation> StandingDataPcRelations { get; set; }
        public DbSet<StandingData> StandingDatas { get; set; }
        public DbSet<SummerizedData> SummerizedDatas { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<QryRoleFeature> QryRoleFeatures { get; set; }
        public DbSet<MapSummary> MapSummaries { get; set; }
    
        [EdmFunction("AppModelContainer", "FunGetPointsByUser")]
        public virtual IQueryable<Nullable<int>> FunGetPointsByUser(Nullable<int> areaId, Nullable<int> locationId, Nullable<int> pointId, Nullable<int> userId)
        {
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var locationIdParameter = locationId.HasValue ?
                new ObjectParameter("LocationId", locationId) :
                new ObjectParameter("LocationId", typeof(int));
    
            var pointIdParameter = pointId.HasValue ?
                new ObjectParameter("PointId", pointId) :
                new ObjectParameter("PointId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<int>>("[AppModelContainer].[FunGetPointsByUser](@AreaId, @LocationId, @PointId, @UserId)", areaIdParameter, locationIdParameter, pointIdParameter, userIdParameter);
        }
    
        [EdmFunction("AppModelContainer", "FunGetProjectsByUser")]
        public virtual IQueryable<Nullable<int>> FunGetProjectsByUser(Nullable<int> projectId, Nullable<int> userId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<int>>("[AppModelContainer].[FunGetProjectsByUser](@ProjectId, @UserId)", projectIdParameter, userIdParameter);
        }
    
        [EdmFunction("AppModelContainer", "FunGetSourceOfFundByUser")]
        public virtual IQueryable<Nullable<int>> FunGetSourceOfFundByUser(Nullable<int> sourceOfFundId, Nullable<int> userId)
        {
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<int>>("[AppModelContainer].[FunGetSourceOfFundByUser](@SourceOfFundId, @UserId)", sourceOfFundIdParameter, userIdParameter);
        }
    
        public virtual int SprAssetEntryComplete(Nullable<int> assetId, ObjectParameter errorCode)
        {
            var assetIdParameter = assetId.HasValue ?
                new ObjectParameter("AssetId", assetId) :
                new ObjectParameter("AssetId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprAssetEntryComplete", assetIdParameter, errorCode);
        }
    
        public virtual int SprDepreciationCharge(Nullable<int> depreciationScheduleId, Nullable<int> userId, ObjectParameter errorCode)
        {
            var depreciationScheduleIdParameter = depreciationScheduleId.HasValue ?
                new ObjectParameter("DepreciationScheduleId", depreciationScheduleId) :
                new ObjectParameter("DepreciationScheduleId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprDepreciationCharge", depreciationScheduleIdParameter, userIdParameter, errorCode);
        }
    
        public virtual int SprDepreciationChargeCalculate(Nullable<int> depreciationMethodId, Nullable<System.DateTime> date, Nullable<System.DateTime> fromDate, Nullable<int> areaId, Nullable<int> depreciationScheduleId, ObjectParameter error)
        {
            var depreciationMethodIdParameter = depreciationMethodId.HasValue ?
                new ObjectParameter("DepreciationMethodId", depreciationMethodId) :
                new ObjectParameter("DepreciationMethodId", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var depreciationScheduleIdParameter = depreciationScheduleId.HasValue ?
                new ObjectParameter("DepreciationScheduleId", depreciationScheduleId) :
                new ObjectParameter("DepreciationScheduleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprDepreciationChargeCalculate", depreciationMethodIdParameter, dateParameter, fromDateParameter, areaIdParameter, depreciationScheduleIdParameter, error);
        }
    
        public virtual int SprDepreciationRollback(Nullable<int> depreciationScheduleId, Nullable<int> userId, ObjectParameter isSuccess)
        {
            var depreciationScheduleIdParameter = depreciationScheduleId.HasValue ?
                new ObjectParameter("DepreciationScheduleId", depreciationScheduleId) :
                new ObjectParameter("DepreciationScheduleId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprDepreciationRollback", depreciationScheduleIdParameter, userIdParameter, isSuccess);
        }
    
        public virtual ObjectResult<string> SprGetBarCode(Nullable<int> pointId, Nullable<int> projectId, Nullable<int> sourceOfFundId, Nullable<int> productId, Nullable<int> quantity, ObjectParameter errorCode)
        {
            var pointIdParameter = pointId.HasValue ?
                new ObjectParameter("PointId", pointId) :
                new ObjectParameter("PointId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("Quantity", quantity) :
                new ObjectParameter("Quantity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SprGetBarCode", pointIdParameter, projectIdParameter, sourceOfFundIdParameter, productIdParameter, quantityParameter, errorCode);
        }
    
        public virtual int SprGetSerial(Nullable<int> areaId, string codeFor, ObjectParameter serialNo)
        {
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var codeForParameter = codeFor != null ?
                new ObjectParameter("CodeFor", codeFor) :
                new ObjectParameter("CodeFor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprGetSerial", areaIdParameter, codeForParameter, serialNo);
        }
    
        public virtual int SprMovementComplete(Nullable<int> userId, Nullable<int> movementId, ObjectParameter errorCode)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var movementIdParameter = movementId.HasValue ?
                new ObjectParameter("MovementId", movementId) :
                new ObjectParameter("MovementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprMovementComplete", userIdParameter, movementIdParameter, errorCode);
        }
    
        public virtual int SprMovementOutsideComplete(Nullable<int> userId, Nullable<int> movementId, Nullable<System.DateTime> date, ObjectParameter errorCode)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var movementIdParameter = movementId.HasValue ?
                new ObjectParameter("MovementId", movementId) :
                new ObjectParameter("MovementId", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprMovementOutsideComplete", userIdParameter, movementIdParameter, dateParameter, errorCode);
        }
    
        public virtual ObjectResult<SprRptAssetsDepreciationSchedule_Result> SprRptAssetsDepreciationSchedule(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> areaId, Nullable<int> userId, ObjectParameter areaName)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptAssetsDepreciationSchedule_Result>("SprRptAssetsDepreciationSchedule", fromDateParameter, toDateParameter, areaIdParameter, userIdParameter, areaName);
        }
    
        public virtual ObjectResult<SprRptAssetsExpired_Result> SprRptAssetsExpired(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> areaId, Nullable<int> locationId, Nullable<int> pointId, Nullable<int> projectId, Nullable<int> sourceOfFundId, Nullable<int> userId, ObjectParameter areaName)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var locationIdParameter = locationId.HasValue ?
                new ObjectParameter("LocationId", locationId) :
                new ObjectParameter("LocationId", typeof(int));
    
            var pointIdParameter = pointId.HasValue ?
                new ObjectParameter("PointId", pointId) :
                new ObjectParameter("PointId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptAssetsExpired_Result>("SprRptAssetsExpired", fromDateParameter, toDateParameter, areaIdParameter, locationIdParameter, pointIdParameter, projectIdParameter, sourceOfFundIdParameter, userIdParameter, areaName);
        }
    
        public virtual ObjectResult<SprRptAssetsInventory_Result> SprRptAssetsInventory(Nullable<System.DateTime> toDate, Nullable<int> areaId, Nullable<int> locationId, Nullable<int> pointId, Nullable<int> projectId, Nullable<int> sourceOfFundId, Nullable<int> productGroupId, Nullable<int> productCategoryId, Nullable<int> userId, ObjectParameter areaName)
        {
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var locationIdParameter = locationId.HasValue ?
                new ObjectParameter("LocationId", locationId) :
                new ObjectParameter("LocationId", typeof(int));
    
            var pointIdParameter = pointId.HasValue ?
                new ObjectParameter("PointId", pointId) :
                new ObjectParameter("PointId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var productGroupIdParameter = productGroupId.HasValue ?
                new ObjectParameter("ProductGroupId", productGroupId) :
                new ObjectParameter("ProductGroupId", typeof(int));
    
            var productCategoryIdParameter = productCategoryId.HasValue ?
                new ObjectParameter("ProductCategoryId", productCategoryId) :
                new ObjectParameter("ProductCategoryId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptAssetsInventory_Result>("SprRptAssetsInventory", toDateParameter, areaIdParameter, locationIdParameter, pointIdParameter, projectIdParameter, sourceOfFundIdParameter, productGroupIdParameter, productCategoryIdParameter, userIdParameter, areaName);
        }
    
        public virtual ObjectResult<SprRptAssetsPurchase_Result> SprRptAssetsPurchase(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> areaId, Nullable<int> locationId, Nullable<int> pointId, Nullable<int> projectId, Nullable<int> sourceOfFundId, Nullable<int> productGroupId, Nullable<int> productCategoryId, Nullable<int> userId, ObjectParameter areaName)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var locationIdParameter = locationId.HasValue ?
                new ObjectParameter("LocationId", locationId) :
                new ObjectParameter("LocationId", typeof(int));
    
            var pointIdParameter = pointId.HasValue ?
                new ObjectParameter("PointId", pointId) :
                new ObjectParameter("PointId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var productGroupIdParameter = productGroupId.HasValue ?
                new ObjectParameter("ProductGroupId", productGroupId) :
                new ObjectParameter("ProductGroupId", typeof(int));
    
            var productCategoryIdParameter = productCategoryId.HasValue ?
                new ObjectParameter("ProductCategoryId", productCategoryId) :
                new ObjectParameter("ProductCategoryId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptAssetsPurchase_Result>("SprRptAssetsPurchase", fromDateParameter, toDateParameter, areaIdParameter, locationIdParameter, pointIdParameter, projectIdParameter, sourceOfFundIdParameter, productGroupIdParameter, productCategoryIdParameter, userIdParameter, areaName);
        }
    
        public virtual ObjectResult<SprRptAssetsValuation_Result> SprRptAssetsValuation(Nullable<System.DateTime> date, Nullable<int> areaId, Nullable<int> projectId, Nullable<int> sourceOfFundId, Nullable<int> productGroupId, Nullable<int> productCategoryId, Nullable<int> userId, ObjectParameter areaName, ObjectParameter projectName, ObjectParameter sourceOfFundName, ObjectParameter productGroupName, ObjectParameter productCategoryName)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var sourceOfFundIdParameter = sourceOfFundId.HasValue ?
                new ObjectParameter("SourceOfFundId", sourceOfFundId) :
                new ObjectParameter("SourceOfFundId", typeof(int));
    
            var productGroupIdParameter = productGroupId.HasValue ?
                new ObjectParameter("ProductGroupId", productGroupId) :
                new ObjectParameter("ProductGroupId", typeof(int));
    
            var productCategoryIdParameter = productCategoryId.HasValue ?
                new ObjectParameter("ProductCategoryId", productCategoryId) :
                new ObjectParameter("ProductCategoryId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptAssetsValuation_Result>("SprRptAssetsValuation", dateParameter, areaIdParameter, projectIdParameter, sourceOfFundIdParameter, productGroupIdParameter, productCategoryIdParameter, userIdParameter, areaName, projectName, sourceOfFundName, productGroupName, productCategoryName);
        }
    
        public virtual ObjectResult<SprRptProductMatrix_Result> SprRptProductMatrix(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprRptProductMatrix_Result>("SprRptProductMatrix", userIdParameter);
        }
    
        public virtual int SprProjectsUpdate(Nullable<int> projectId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SprProjectsUpdate", projectIdParameter);
        }
    
        public virtual ObjectResult<SprMailSend_Result> SprMailSend()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SprMailSend_Result>("SprMailSend");
        }
    
        public virtual int GetCSV()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetCSV");
        }
    
        public virtual ObjectResult<SP_GenerateMapSummary_Result> SP_GenerateMapSummary()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GenerateMapSummary_Result>("SP_GenerateMapSummary");
        }
    }
}
