using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using System.Transactions;
namespace Portal.Core
{
    public class StockLedgerBL
    {
        
        public StockLedgerBL()
        {
        
        }
   
        public DataTable GetStockLedgerDataTable(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtStockLedger = new DataTable();
            try
            {
                dtStockLedger = sqlDbInterface.GetStockLedgerDetail(productTypeId,  assemblyType,  productMainGroupId, productSubGroupId,  productId, customerBranchId, fromDate, toDate,companyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtStockLedger;
        }

        public DataTable GetStockSummaryDataTable(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtStockLedger = new DataTable();
            try
            {
                dtStockLedger = sqlDbInterface.GetStockLedgerSummary(productTypeId, assemblyType, productMainGroupId, productSubGroupId, productId, customerBranchId, fromDate, toDate, companyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtStockLedger;
        }
        public decimal GetProductAvailableStock(long productId, int finYearId, int companyId, int companyBranchId, int trnId, string trnType)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            decimal availableStock = 0;
            try
            {
                availableStock = sqlDbInterface.GetProductAvailableStock(productId, finYearId,  companyId,  companyBranchId, trnId, trnType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return availableStock;
        }


    }
}
