using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IBillService
    {
        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize);

        void Create(BillViewModel billVm);

        void Update(BillViewModel billVm);

        BillViewModel GetDetail(int billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(int productId, int billId, int colorId, int sizeId);

        void UpdateStatus(int orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(int billId);

        List<ColorViewModel> GetColors();

        List<SizeViewModel> GetSizes();

        ColorViewModel GetColor(int id);

        SizeViewModel GetSize(int id);

        void Save();
    }
}
