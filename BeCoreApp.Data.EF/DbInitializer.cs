using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Constants;

namespace BeCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    //Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123456789");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "No 36 Lane 133 Nguyen Phong Sac Cau Giay",
                    Email = "pandashop@gmail.com",
                    Name = "Panda Shop",
                    Phone = "0942 324 543",
                    Status = Status.Active,
                    Website = "http://pandashop.com",
                    Lat = 21.0435009,
                    Lng = 105.7894758
                });
            }

            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM_MANAGEMENT", Name = "Hệ Thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "flaticon-settings-1"  },
                    new Function() {Id = "ROLE", Name = "Quyền",ParentId = "SYSTEM_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = ""  },
                    new Function() {Id = "FUNCTION", Name = "Chức Năng",ParentId = "SYSTEM_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = ""  },
                    new Function() {Id = "USER", Name = "Thành Viên",ParentId = "SYSTEM_MANAGEMENT",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = ""  },
                    new Function() {Id = "ACTIVITY", Name = "Hoạt Động",ParentId = "SYSTEM_MANAGEMENT",SortOrder = 4,Status = Status.Active,URL = "/admin/activity/index",IconCss = ""  },
                    new Function() {Id = "ERROR", Name = "Lỗi",ParentId = "SYSTEM_MANAGEMENT",SortOrder = 5,Status = Status.Active,URL = "/admin/error/index",IconCss = ""  },
                    new Function() {Id = "SETTING", Name = "Cấu Hình",ParentId = "SYSTEM_MANAGEMENT",SortOrder = 6,Status = Status.Active,URL = "/admin/setting/index",IconCss = ""  },

                    new Function() {Id = "PRODUCT_MANAGEMENT",Name = "Quản Lý Sản Phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "flaticon-interface-9"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Loại",ParentId = "PRODUCT_MANAGEMENT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = ""  },
                    new Function() {Id = "PRODUCT",Name = "Sản Phẩm",ParentId = "PRODUCT_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = ""  },
                    new Function() {Id = "BILL",Name = "Hóa Đơn",ParentId = "PRODUCT_MANAGEMENT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = ""  },

                    new Function() {Id = "CONTENT",Name = "Post",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "flaticon-share"  },
                    new Function() {Id = "MENUGROUP",Name = "Menu Group",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/menugroup/index",IconCss = ""  },
                    new Function() {Id = "MENUITEM",Name = "Menu Item",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/menuitem/index",IconCss = ""  },
                    new Function() {Id = "BLOGCATEGORY",Name = "Blog Category",ParentId = "CONTENT",SortOrder = 3,Status = Status.Active,URL = "/admin/blogcategory/index",IconCss = ""  },
                    new Function() {Id = "BLOG",Name = "Blog",ParentId = "CONTENT",SortOrder = 4,Status = Status.Active,URL = "/admin/blog/index",IconCss = ""  },
                    new Function() {Id = "PAGE",Name = "Trang Đơn",ParentId = "CONTENT",SortOrder = 5,Status = Status.Active,URL = "/admin/page/index",IconCss = ""  },

                    new Function() {Id = "UTILITY_MANAGEMENT",Name = "Tiện Ích",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "flaticon-grid-menu-v2"  },
                    new Function() {Id = "FOOTER",Name = "Footer",ParentId = "UTILITY_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/footer/index",IconCss = ""  },
                    new Function() {Id = "FEEDBACK",Name = "Phản Hồi",ParentId = "UTILITY_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/feedback/index",IconCss = ""  },
                    new Function() {Id = "ANNOUNCEMENT",Name = "Announcement",ParentId = "UTILITY_MANAGEMENT",SortOrder = 3,Status = Status.Active,URL = "/admin/announcement/index",IconCss = ""  },
                    new Function() {Id = "CONTACT",Name = "Liên Hệ",ParentId = "UTILITY_MANAGEMENT",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = ""  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY_MANAGEMENT",SortOrder = 5,Status = Status.Active,URL = "/admin/slide/index",IconCss = ""  },
                    new Function() {Id = "ADVERTISMENT",Name = "Advertisment",ParentId = "UTILITY_MANAGEMENT",SortOrder = 6,Status = Status.Active,URL = "/admin/advertistment/index",IconCss = ""  },

                    new Function() {Id = "REPORT_MANAGEMENT",Name = "Thống Kê",ParentId = null,SortOrder = 5,Status = Status.Active,URL = "/",IconCss = "flaticon-line-graph"  },
                    new Function() {Id = "REVENUES",Name = "Revenue report",ParentId = "REPORT_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/report/revenues",IconCss = ""  },
                    new Function() {Id = "ACCESS",Name = "Visitor Report",ParentId = "REPORT_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/report/visitor",IconCss = ""  },
                    new Function() {Id = "READER",Name = "Reader Report",ParentId = "REPORT_MANAGEMENT",SortOrder = 3,Status = Status.Active,URL = "/admin/report/reader",IconCss = ""  },

                    new Function() {Id = "LOCATION_MANAGEMENT", Name = "Quản Lý Vị Trí",ParentId = null,SortOrder = 5,Status = Status.Active,URL = "/",IconCss = "flaticon-map-location"  },
                    new Function() {Id = "PROVINCE", Name = "Tỉnh/Thành Phố",ParentId = "LOCATION_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/province/index",IconCss = ""  },
                    new Function() {Id = "DISTRICT", Name = "Quận/Huyện",ParentId = "LOCATION_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/district/index",IconCss = ""  },
                    new Function() {Id = "WARD", Name = "Xã/Phường",ParentId = "LOCATION_MANAGEMENT",SortOrder = 3,Status = Status.Active,URL = "/admin/ward/index",IconCss = ""  },
                    new Function() {Id = "STREET", Name = "Đường",ParentId = "LOCATION_MANAGEMENT",SortOrder = 4,Status = Status.Active,URL = "/admin/street/index",IconCss = ""  },
                    new Function() {Id = "DIRECTION", Name = "Hướng Nhà",ParentId = "LOCATION_MANAGEMENT",SortOrder = 5,Status = Status.Active,URL = "/admin/direction/index",IconCss = ""  },

                    new Function() {Id = "REALESTATE_MANAGEMENT", Name = "Quản Lý Rao Vặt",ParentId = null,SortOrder = 6,Status = Status.Active,URL = "/",IconCss = "flaticon-file"  },
                    new Function() {Id = "CLASSIFIED_CATEGORY", Name = "Loại",ParentId = "REALESTATE_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/classifiedcategory/index",IconCss = ""  },
                    new Function() {Id = "CLASSIFIED", Name = "Rao Vặt",ParentId = "REALESTATE_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/classified/index",IconCss = ""  },
                    new Function() {Id = "TYPE", Name = "Hình Thức",ParentId = "REALESTATE_MANAGEMENT",SortOrder = 3,Status = Status.Active,URL = "/admin/type/index",IconCss = ""  },
                    new Function() {Id = "UNIT", Name = "Đơn Vị",ParentId = "REALESTATE_MANAGEMENT",SortOrder = 4,Status = Status.Active,URL = "/admin/unit/index",IconCss = ""  },

                    new Function() {Id = "PROJECT_MANAGEMENT", Name = "Quản Lý Dự Án",ParentId = null,SortOrder = 7,Status = Status.Active,URL = "/",IconCss = "flaticon-pie-chart"  },
                    new Function() {Id = "PROJECT_CATEGORY", Name = "Loại Dự Án",ParentId = "PROJECT_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/projectcategory/index",IconCss = ""  },
                    new Function() {Id = "PROJECT", Name = "Dự Án",ParentId = "PROJECT_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/project/index",IconCss = ""  },

                    new Function() {Id = "ENTERPRISE_MANAGEMENT", Name = "Quản Lý Doanh Nghiệp",ParentId = null,SortOrder = 8,Status = Status.Active,URL = "/",IconCss = "flaticon-signs"  },
                    new Function() {Id = "FIELD", Name = "Lĩnh Vực",ParentId = "ENTERPRISE_MANAGEMENT",SortOrder = 1,Status = Status.Active,URL = "/admin/field/index",IconCss = ""  },
                    new Function() {Id = "ENTERPRISE", Name = "Doanh Nghiệp",ParentId = "ENTERPRISE_MANAGEMENT",SortOrder = 2,Status = Status.Active,URL = "/admin/enterprise/index",IconCss = ""  },
                });
            }

            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = "Footer"
                });
            }

            if (_context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                    new Color() {Name="Black", Code="#000000" },
                    new Color() {Name="White", Code="#FFFFFF"},
                    new Color() {Name="Red", Code="#ff0000" },
                    new Color() {Name="Blue", Code="#1000ff" },
                };
                _context.Colors.AddRange(listColor);
            }
            if (_context.AdvertistmentPages.Count() == 0)
            {
                List<AdvertistmentPage> pages = new List<AdvertistmentPage>()
                {
                    new AdvertistmentPage() {Id="home", Name="Home",
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="home-left",Name="Bên trái"}
                    } },
                    new AdvertistmentPage() {Id="product-cate", Name="Product category",
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-cate-left",Name="Bên trái"}
                    }},
                    new AdvertistmentPage() {Id="product-detail", Name="Product detail",
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-detail-left",Name="Bên trái"}
                    } }
                };
                _context.AdvertistmentPages.AddRange(pages);
            }

            if (_context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="XXL" },
                    new Size() { Name="XL"},
                    new Size() { Name="L" },
                    new Size() { Name="M" },
                    new Size() { Name="S" },
                    new Size() { Name="XS" }
                };
                _context.Sizes.AddRange(listSize);
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Home's title",
                    Value1 = "Tedu Shop home",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Home Keyword",
                    Value1 = "shopping, sales",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Home Description",
                    Value1 = "Home tedu",
                    Status = Status.Active
                });
            }

            await _context.SaveChangesAsync();

        }
    }
}