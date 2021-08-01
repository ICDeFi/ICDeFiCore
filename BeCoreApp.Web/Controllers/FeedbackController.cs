using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeCoreApp.Controllers
{
    public class FeedbackController : Controller
    {
        public IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public IActionResult SaveEntity(FeedbackViewModel model)
        {
            try
            {
                _feedbackService.Add(model);
                _feedbackService.SaveChanges();
                return new OkObjectResult(true);
            }
            catch (Exception)
            {
                return new OkObjectResult(false);
            }
        }

    }
}