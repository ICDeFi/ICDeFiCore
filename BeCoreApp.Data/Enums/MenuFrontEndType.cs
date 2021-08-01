using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeCoreApp.Data.Enums
{
    public enum MenuFrontEndType
    {
        [Description("None")]
        None = -1,
        [Description("Trang Chủ")]
        TrangChu = 1,
        [Description("Bài Viết")]
        BaiViet = 2,
        [Description("Liên Hệ")]
        LienHe = 3,
        [Description("Sản Phẩm")]
        SanPham = 4
    }
}
