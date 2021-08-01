using AutoMapper;
using BeCoreApp.Application.ViewModels;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Application.ViewModels.RealEstate;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<WholePrice, WholePriceViewModel>().MaxDepth(2);
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);
            CreateMap<Contact, ContactViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);
            CreateMap<Province, ProvinceViewModel>();
            CreateMap<District, DistrictViewModel>();
            CreateMap<Ward, WardViewModel>();
            CreateMap<Street, StreetViewModel>();
            CreateMap<BeCoreApp.Data.Entities.Type, TypeViewModel>();
            CreateMap<Unit, UnitViewModel>();
            CreateMap<ClassifiedCategory, ClassifiedCategoryViewModel>();
            CreateMap<Direction, DirectionViewModel>();
            CreateMap<ProjectCategory, ProjectCategoryViewModel>();
            CreateMap<Field, FieldViewModel>();
            CreateMap<Enterprise, EnterpriseViewModel>();
            CreateMap<EnterpriseField, EnterpriseFieldViewModel>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<ProjectImage, ProjectImageViewModel>();
            CreateMap<ProjectLibrary, ProjectLibraryViewModel>();
        }
    }
}
