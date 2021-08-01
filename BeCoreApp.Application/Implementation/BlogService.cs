using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;

namespace BeCoreApp.Application.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IBlogRepository blogRepository, IBlogCategoryService blogCategoryService,
        IBlogTagRepository blogTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _blogCategoryService = blogCategoryService;
            _blogRepository = blogRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public void CheckSeo(BlogViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            modeVm.SeoAlias = TextHelper.UrlFriendly(modeVm.Name);
        }

        public BlogViewModel Add(BlogViewModel blogVm)
        {
            CheckSeo(blogVm);

            var blog = new Blog
            {
                Id = blogVm.Id,
                Name = blogVm.Name,
                Image = blogVm.Image,
                Description = blogVm.Description,
                Like = blogVm.Like,
                Comment = blogVm.Comment,
                Share = blogVm.Share,
                Video = blogVm.Video,
                MildContent = blogVm.MildContent,
                ReferralLinkRule = blogVm.ReferralLinkRule,
                ReferralLink = blogVm.ReferralLink,
                HomeFlag = blogVm.HomeFlag,
                HotFlag = blogVm.HotFlag,
                Status = blogVm.Status,
                SeoPageTitle = blogVm.SeoPageTitle,
                SeoAlias = blogVm.SeoAlias,
                SeoKeywords = blogVm.SeoKeywords,
                SeoDescription = blogVm.SeoDescription,
                BlogCategoryId = blogVm.BlogCategoryId,
                ViewCount = blogVm.ViewCount
            };

            if (blogVm.Tags.Count() > 0)
            {
                blog.BlogTags = new List<BlogTag>();

                foreach (string t in blogVm.Tags.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId && x.Type == CommonConstants.BlogTag).Any())
                    {
                        _tagRepository.Add(new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.BlogTag
                        });
                    }

                    blog.BlogTags.Add(new BlogTag
                    {
                        TagId = tagId
                    });
                }
            }

            _blogRepository.Add(blog);
            return blogVm;
        }

        public void Delete(int id) => _blogRepository.Remove(id);

        public List<BlogViewModel> GetLatestBlogs(int top)
        {
            return _blogRepository.FindAll(c => c.BlogTags).OrderByDescending(x => x.DateModified).Take(top)
            .Select(x => new BlogViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Like = x.Like,
                Comment = x.Comment,
                Share = x.Share,
                Video = x.Video,
                MildContent = x.MildContent,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                Status = x.Status,
                SeoPageTitle = x.SeoPageTitle,
                SeoAlias = x.SeoAlias,
                SeoKeywords = x.SeoKeywords,
                SeoDescription = x.SeoDescription,
                BlogCategoryId = x.BlogCategoryId,
                BlogCategoryName = x.BlogCategory.Name,
                DateModified = x.DateModified,
                DateCreated = x.DateCreated,
                ViewCount = x.ViewCount
            }).ToList();
        }

        public List<BlogViewModel> GetHomeBlogs()
        {
            var query = _blogRepository.FindAll(x => x.HomeFlag == true, bc => bc.BlogCategory)
                .OrderByDescending(x => x.Id)
                .Select(x => new BlogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoAlias = x.SeoAlias,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    DateModified = x.DateModified,
                    DateCreated = x.DateCreated,
                    ViewCount = x.ViewCount,
                    BlogCategory = new BlogCategoryViewModel
                    {
                        Id = x.BlogCategory.Id,
                        URL = x.BlogCategory.URL,
                        Name = x.BlogCategory.Name
                    }
                }).ToList();

            return query;
        }


        public PagedResult<BlogViewModel> GetAllPaging(string startDate, string endDate, string keyword, int blogCategoryId, int pageIndex, int pageSize)
        {
            var query = _blogRepository.FindAll(x => x.BlogCategory);

            if (!string.IsNullOrWhiteSpace(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated >= start);
            }

            if (!string.IsNullOrWhiteSpace(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated <= end);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));

            if (blogCategoryId != 0)
            {
                var blogCategoryList = _blogCategoryService.GetBlogCategoryIdsById(blogCategoryId, MenuFrontEndType.BaiViet);
                query = query.Where(x => blogCategoryList.Contains(x.BlogCategoryId));
            }

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new BlogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                    MildContent = x.MildContent,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoAlias = x.SeoAlias,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    BlogCategoryId = x.BlogCategoryId,
                    BlogCategoryName = x.BlogCategory.Name,
                    DateModified = x.DateModified,
                    DateCreated = x.DateCreated,
                    ViewCount = x.ViewCount
                }).ToList();

            return new PagedResult<BlogViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public BlogViewModel GetById(int id)
        {
            var model = _blogRepository.FindById(id, x => x.BlogCategory, a => a.BlogTags);
            if (model == null)
                return null;

            return new BlogViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Description = model.Description,
                Like = model.Like,
                Comment = model.Comment,
                Share = model.Share,
                Video = model.Video,
                MildContent = model.MildContent,
                ReferralLink = model.ReferralLink,
                ReferralLinkRule = model.ReferralLinkRule,
                HomeFlag = model.HomeFlag,
                HotFlag = model.HotFlag,
                Status = model.Status,
                SeoPageTitle = model.SeoPageTitle,
                SeoAlias = model.SeoAlias,
                SeoKeywords = model.SeoKeywords,
                SeoDescription = model.SeoDescription,
                BlogCategoryId = model.BlogCategoryId,
                BlogCategoryName = model.BlogCategory.Name,
                ViewCount = model.ViewCount,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                BlogTags = model.BlogTags
                                .Select(x => new BlogTagViewModel()
                                {
                                    Id = x.Id,
                                    TagId = x.TagId,
                                    BlogId = x.BlogId
                                }).ToList()
            };
        }

        public void Save() => _unitOfWork.Commit();

        public void UpdateViewCount(int id)
        {
            var blog = _blogRepository.FindById(id);
            if (blog != null)
            {
                if (blog.ViewCount.HasValue)
                    blog.ViewCount += 1;
                else
                    blog.ViewCount = 1;

                _blogRepository.Update(blog);
                Save();
            }
        }

        public void Update(BlogViewModel blogVm)
        {
            CheckSeo(blogVm);

            var blog = new Blog
            {
                Id = blogVm.Id,
                Name = blogVm.Name,
                Image = blogVm.Image,
                Description = blogVm.Description,
                Like = blogVm.Like,
                Comment = blogVm.Comment,
                Share = blogVm.Share,
                Video = blogVm.Video,
                MildContent = blogVm.MildContent,
                ReferralLinkRule = blogVm.ReferralLinkRule,
                ReferralLink = blogVm.ReferralLink,
                HomeFlag = blogVm.HomeFlag,
                HotFlag = blogVm.HotFlag,
                Status = blogVm.Status,
                SeoPageTitle = blogVm.SeoPageTitle,
                SeoAlias = blogVm.SeoAlias,
                SeoKeywords = blogVm.SeoKeywords,
                SeoDescription = blogVm.SeoDescription,
                BlogCategoryId = blogVm.BlogCategoryId,
                ViewCount = blogVm.ViewCount
            };

            var blogTagRemoves = _blogTagRepository.FindAll(x => x.BlogId == blogVm.Id).ToList();
            _blogTagRepository.RemoveMultiple(blogTagRemoves);

            if (blogVm.Tags.Count() > 0)
            {
                foreach (string t in blogVm.Tags.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId && x.Type == CommonConstants.BlogTag).Any())
                    {
                        _tagRepository.Add(new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.BlogTag
                        });
                    }

                    _blogTagRepository.Add(new BlogTag { BlogId = blogVm.Id, TagId = tagId });
                }
            }

            _blogRepository.Update(blog);
        }

        public TagViewModel GetTagById(string id)
        {
            var model = _tagRepository.FindById(id);
            if (model == null)
                return null;

            return new TagViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };
        }

        public List<TagViewModel> GetListTagByBlogId(int blogId)
        {
            return _blogTagRepository.FindAll(x => x.BlogId == blogId, c => c.Tag)
            .Select(x => x.Tag).Select(x => new TagViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type
            }).ToList();
        }

        public List<TagViewModel> GetListTagByType(string tagType)
        {
            return _tagRepository.FindAll(x => x.Type == tagType)
                .Select(x => new TagViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                }).ToList();
        }

        public PagedResult<BlogViewModel> GetAllByTagId(string tagId, int pageIndex, int pageSize)
        {
            var query = from p in _blogRepository.FindAll()
                        join pt in _blogTagRepository.FindAll()
                        on p.Id equals pt.BlogId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new BlogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                    Like = x.Like,
                    Comment = x.Comment,
                    Share = x.Share,
                    Video = x.Video,
                    MildContent = x.MildContent,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoAlias = x.SeoAlias,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    BlogCategoryId = x.BlogCategoryId,
                    BlogCategoryName = x.BlogCategory.Name,
                    DateModified = x.DateModified,
                    DateCreated = x.DateCreated,
                    ViewCount = x.ViewCount
                }).ToList();

            return new PagedResult<BlogViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<BlogViewModel> GetBlogRelatesById(int id)
        {
            var model = _blogRepository.FindById(id);
            var query = _blogRepository.FindAll(x => x.BlogCategoryId == model.BlogCategoryId);
            var nextModels = query.Where(x => x.Id > model.Id).Take(2).ToList();
            nextModels.Add(model);
            var previousModels = query.Where(x => x.Id < model.Id).OrderByDescending(x => x.Id).Take(2).ToList();
            nextModels.AddRange(previousModels);

            return nextModels.OrderByDescending(x => x.Id).Select(x => new BlogViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Like = x.Like,
                Comment = x.Comment,
                Share = x.Share,
                Video = x.Video,
                MildContent = x.MildContent,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                Status = x.Status,
                SeoPageTitle = x.SeoPageTitle,
                SeoAlias = x.SeoAlias,
                SeoKeywords = x.SeoKeywords,
                SeoDescription = x.SeoDescription,
                BlogCategoryId = x.BlogCategoryId,
                BlogCategoryName = x.BlogCategory.Name,
                DateModified = x.DateModified,
                DateCreated = x.DateCreated,
                ViewCount = x.ViewCount,
            }).ToList();
        }
    }
}