public async Task<CustomerSummaryModel> GetWorkFlowStagesListByUserIDAsync(int userID, string dataBaseName, int workFlowID, int? filterbranchID, int? filterUserID)
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(dataBaseName.ToUpper()));
            string sql = $"EXEC {dataBaseName}.dbo.proc_WorkFlow_Increase_Tab @FilterBranchID , @FilterUserID , @UserID " +
                         $"EXEC {dataBaseName.ToUpper()}.dbo.proc_WorkFlow_Increase_Tab @FilterBranchID , @FilterUserID , @UserID " +
                         $"EXEC  TritonSecurity.dbo.proc_Branches_Select  " +
                         $"EXEC {dataBaseName.ToUpper()}.dbo.proc_RepCodes_Select  " +
                         $"EXEC {dataBaseName.ToUpper()}.dbo.proc_Customers_SaleRep_WorkStage @FilterBranchID, @FilterUserID , @UserID ";


            var customersModel = new CustomerSummaryModel();

            using (var multi = connection.QueryMultiple(sql, new { UserID = userID, DataBaseName = dataBaseName, WorkFlowID = workFlowID, FilterBranchID = filterbranchID, FilterUserID = filterUserID }))
            {
                customersModel.WorkFlowStagesList = multi.Read<WorkFlowStagesModel>().ToList();
                customersModel.OverViewTabsList = multi.Read<WorkFlowStagesModel>().ToList();
                customersModel.BranchesList = multi.Read<Branches>().ToList();
                customersModel.RepCodesList = multi.Read<RepCodesModel>().ToList();
                customersModel.proc_Customers_SalesRep_WorkStagesList = multi.Read<proc_Customers_SalesRep_WorkStage>().ToList();
            }
            return customersModel;
        }

