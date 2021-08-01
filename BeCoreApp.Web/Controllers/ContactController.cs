using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Models;
using BeCoreApp.Services;
using BeCoreApp.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BeCoreApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IFeedbackService _feedbackService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;

        public ContactController(IContactService contactSerivce,
            IViewRenderService viewRenderService,
            IConfiguration configuration,
            IEmailSender emailSender, IFeedbackService feedbackService)
        {
            _contactService = contactSerivce;
            _feedbackService = feedbackService;
            _emailSender = emailSender;
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }
        [Route("lien-he.html")]
        [HttpGet]
        public IActionResult Index()
        {
            var contact = _contactService.GetById(CommonConstants.DefaultContactId);
            var model = new ContactPageViewModel { Contact = contact };
            return View(model);
        }

        [Route("lien-he.html")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(ContactPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.Add(model.Feedback);
                _feedbackService.SaveChanges();
                var content = await _viewRenderService.RenderToStringAsync("Contact/_ContactMail", model.Feedback);
                await _emailSender.SendEmailAsync(_configuration["MailSettings:AdminMail"], "Have new contact feedback", content);
                ViewData["Success"] = true;
            }

            model.Contact = _contactService.GetById("default");

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(FeedbackViewModel model)
        {
            try
            {
                _feedbackService.Add(model);
                _feedbackService.SaveChanges();

                var content = await _viewRenderService.RenderToStringAsync("Contact/_ContactMail", model);
                await _emailSender.SendEmailAsync(_configuration["MailSettings:AdminMail"], "Contact from website icdefi.org", content);

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(false);
            }
        }

        [HttpGet]
        public IActionResult ContactMail()
        {
            return View("_ContactMail");
        }
    }
}