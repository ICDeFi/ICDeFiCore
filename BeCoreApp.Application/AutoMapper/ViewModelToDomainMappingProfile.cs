using AutoMapper;
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

namespace BeCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id,
              c.CustomerName, c.CustomerAddress,
              c.CustomerMobile, c.CustomerMessage, c.BillStatus,
              c.PaymentMethod, c.Status, c.CustomerId));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(
                  c.Id, c.BillId, c.ProductId,
              c.Quantity, c.Price, c.ColorId, c.SizeId));

            CreateMap<ContactViewModel, Contact>()
                .ConstructUsing(c => new Contact(c.Id, c.Name, c.Phone,
                c.Email, c.Website, c.Address, c.Other,
                c.Lng, c.Lat, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<PageViewModel, Page>()
                .ConstructUsing(c => new Page(c.Id, c.Name,
                c.Content, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProvinceViewModel, Province>()
                .ConstructUsing(c => new Province(c.Id,
                c.Name, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<DistrictViewModel, District>()
                .ConstructUsing(c => new District(c.Id, c.Name,
                c.ProvinceId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<WardViewModel, Ward>()
                .ConstructUsing(c => new Ward(c.Id, c.Name,
                c.DistrictId, c.ProvinceId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<StreetViewModel, Street>()
                .ConstructUsing(c => new Street(
                    c.Id, c.Name, c.ProvinceId,
                c.DistrictId, c.WardId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<TypeViewModel, BeCoreApp.Data.Entities.Type>()
                .ConstructUsing(c => new BeCoreApp.Data.Entities.Type(c.Id,
                c.Name, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<UnitViewModel, Unit>()
                .ConstructUsing(c => new Unit(c.Id, c.Name,
                c.TypeId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ClassifiedCategoryViewModel, ClassifiedCategory>()
                .ConstructUsing(c => new ClassifiedCategory(c.Id, c.Name,
                c.TypeId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<DirectionViewModel, Direction>()
                .ConstructUsing(c => new Direction(
                    c.Id, c.Name, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProjectCategoryViewModel, ProjectCategory>()
                .ConstructUsing(c => new ProjectCategory(c.Id,
                c.Name, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<FieldViewModel, Field>()
                .ConstructUsing(c => new Field(
                    c.Id, c.Name, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<EnterpriseViewModel, Enterprise>()
                .ConstructUsing(c => new Enterprise(c.Id, c.Name, c.Image,
                c.Content, c.Phone, c.Email, c.Website, c.Address,
                c.Hotline, c.HomeFlag, c.ProvinceId, c.DistrictId,
                c.WardId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<EnterpriseFieldViewModel, EnterpriseField>()
                .ConstructUsing(c => new EnterpriseField(
                    c.Id, c.EnterpriseId, c.FieldId));

            CreateMap<ProjectViewModel, Project>()
                .ConstructUsing(c => new Project(
                    c.Id, c.Name, c.Image, c.TotalArea,
                c.AreageBuild, c.ProgressBuild, c.ProjectScale,
                c.HandOverTheHouse, c.Content, c.Location, c.Infrastructure,
                c.OverallDiagram, c.GroundDesign,
                c.Video, c.FinancialSupport, c.Address,
                c.HomeFlag, c.ProvinceId, c.DistrictId, c.WardId,
                c.ProjectCategoryId, c.EnterpriseId, c.Status, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProjectImageViewModel, ProjectImage>()
                .ConstructUsing(c => new ProjectImage(c.Id,
                c.Path, c.Caption, c.ProjectId));

            CreateMap<ProjectLibraryViewModel, ProjectLibrary>()
               .ConstructUsing(c => new ProjectLibrary(c.Id,
               c.Path, c.Caption, c.ProjectId));
        }
    }
}