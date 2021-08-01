using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class FunctionService : IFunctionService
    {
        private IFunctionRepository _functionRepository;
        private IUnitOfWork _unitOfWork;

        public FunctionService(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
        }

        public FunctionViewModel Add(FunctionViewModel functionVm)
        {
            var functiondbs = _functionRepository.FindAll(x => x.ParentId == functionVm.ParentId);
            if (functiondbs.Count() > 0)
                functionVm.SortOrder += functiondbs.Max(x => x.SortOrder);



            Function function = new Function
            {
                Id = functionVm.Id.ToUpper(),
                ActionControl = functionVm.ActionControl,
                IconCss = functionVm.IconCss,
                IsFrontEnd = functionVm.IsFrontEnd,
                Name = functionVm.Name,
                ParentId = functionVm.ParentId,
                SortOrder = functionVm.SortOrder,
                Status = functionVm.Status,
                URL = functionVm.URL
            };

            _functionRepository.Add(function);

            return functionVm;
        }

        public void Delete(string id)
        {
            var functionChildrens = _functionRepository.FindAll(x => x.ParentId == id).ToList();
            for (int i = 0; i < functionChildrens.Count(); i++)
            {
                functionChildrens[i].ParentId = null;
                _functionRepository.Update(functionChildrens[i]);
            }

            var db = _functionRepository.FindSingle(x => x.Id == id);
            _functionRepository.Remove(db);
        }

        public List<FunctionTreeViewModel> GetTreeAll(Status isFrontEnd)
        {
            var listData = _functionRepository.FindAll().Where(x => isFrontEnd == Status.NoneActive || x.IsFrontEnd == isFrontEnd)
                .Select(x => new FunctionTreeViewModel()
                {
                    id = x.Id,
                    text = x.Name,
                    icon = "fa fa-folder",
                    state = new FunctionTreeState
                    { opened = true },
                    data = new FunctionTreeData
                    {
                        rootId = x.Id,
                        parentId = x.ParentId,
                        sortOrder = x.SortOrder,
                        actionControl = x.ActionControl,
                        url = x.URL,
                        status = x.Status,
                        iconCss = x.IconCss,
                        isFrontEnd = x.IsFrontEnd
                    }
                });

            var groups = listData.GroupBy(i => i.data.parentId);

            var roots = groups.FirstOrDefault(g => string.IsNullOrWhiteSpace(g.Key)).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => !string.IsNullOrWhiteSpace(g.Key))
                    .ToDictionary(g => g.Key, g => g.ToList());

                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private void AddChildren(FunctionTreeViewModel root, IDictionary<string, List<FunctionTreeViewModel>> source)
        {
            if (source.ContainsKey(root.id))
            {
                root.children = source[root.id].OrderBy(x => x.data.sortOrder).ToList();
                for (int i = 0; i < root.children.Count; i++)
                    AddChildren(root.children[i], source);
            }
            else
            {
                root.icon = "fa fa-file m--font-warning";
                root.children = new List<FunctionTreeViewModel>();
            }
        }

        public List<FunctionViewModel> GetAll()
        {
            var query = _functionRepository.FindAll()
                .Select(function => new FunctionViewModel
                {
                    Id = function.Id,
                    ActionControl = function.ActionControl,
                    IconCss = function.IconCss,
                    IsFrontEnd = function.IsFrontEnd,
                    Name = function.Name,
                    ParentId = function.ParentId,
                    SortOrder = function.SortOrder,
                    Status = function.Status,
                    URL = function.URL,
                }).ToList();

            return query;
        }

        public List<Function> GetAllFunctionToSetPermission()
        {
            var functions = _functionRepository.FindAll()
                .Where(x => x.IsFrontEnd == Status.InActive
                && !string.IsNullOrWhiteSpace(x.ActionControl)
                && !string.IsNullOrWhiteSpace(x.URL) && x.URL != "#").ToList();

            return functions;
        }

        public FunctionViewModel GetById(string id)
        {
            var function = _functionRepository.FindById(id);
            if (function==null)
                return null;

            return new FunctionViewModel
            {
                Id = function.Id,
                ActionControl = function.ActionControl,
                IconCss = function.IconCss,
                IsFrontEnd = function.IsFrontEnd,
                Name = function.Name,
                ParentId = function.ParentId,
                SortOrder = function.SortOrder,
                Status = function.Status,
                URL = function.URL
            };
        }

        public void Update(FunctionViewModel model)
        {
            Function function = new Function
            {
                Id = model.Id.ToUpper(),
                ActionControl = model.ActionControl,
                IconCss = model.IconCss,
                IsFrontEnd = model.IsFrontEnd,
                Name = model.Name,
                ParentId = model.ParentId,
                SortOrder = model.SortOrder,
                Status = model.Status,
                URL = model.URL
            };

            _functionRepository.Update(function);
        }

        public void ReOrder(string sourceId, string targetId)
        {
            var source = _functionRepository.FindById(sourceId);
            var target = _functionRepository.FindById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _functionRepository.Update(source);
            _functionRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateParentId(string id, string newParentId)
        {
            if (newParentId == "#")
                newParentId = null;

            var function = _functionRepository.FindSingle(x => x.Id == id);
            function.SortOrder = 1;
            function.ParentId = newParentId;

            var functiondbs = _functionRepository.FindAll(x => x.ParentId == newParentId);
            if (functiondbs.Count() > 0)
                function.SortOrder += functiondbs.Max(x => x.SortOrder);

            _functionRepository.Update(function);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
