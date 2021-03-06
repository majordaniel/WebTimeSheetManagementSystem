﻿namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="UserController" />
    /// </summary>
    [ValidateUserSession]
    public class UserController : Controller
    {
        /// <summary>
        /// Defines the _ITimeSheet
        /// </summary>
        private readonly ITimeSheet _ITimeSheet;

        /// <summary>
        /// Defines the _IExpense
        /// </summary>
        private readonly IExpense _IExpense;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController()
        {
            _ITimeSheet = new TimeSheetConcrete();
            _IExpense = new ExpenseConcrete();
        }

        // GET: User
        /// <summary>
        /// The Dashboard
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Dashboard()
        {
            var timesheetResult = _ITimeSheet.GetTimeSheetsCountByUserID(Convert.ToString(Session["UserID"]));

            if (timesheetResult != null)
            {
                ViewBag.SubmittedTimesheetCount = timesheetResult.SubmittedCount;
                ViewBag.ApprovedTimesheetCount = timesheetResult.ApprovedCount;
                ViewBag.RejectedTimesheetCount = timesheetResult.RejectedCount;
            }
            else
            {
                ViewBag.SubmittedTimesheetCount = 0;
                ViewBag.ApprovedTimesheetCount = 0;
                ViewBag.RejectedTimesheetCount = 0;
            }

            var expenseResult = _IExpense.GetExpenseAuditCountByUserID(Convert.ToString(Session["UserID"]));

            if (expenseResult != null)
            {
                ViewBag.SubmittedExpenseCount = expenseResult.SubmittedCount;
                ViewBag.ApprovedExpenseCount = expenseResult.ApprovedCount;
                ViewBag.RejectedExpenseCount = expenseResult.RejectedCount;
            }
            else
            {
                ViewBag.SubmittedExpenseCount = 0;
                ViewBag.ApprovedExpenseCount = 0;
                ViewBag.RejectedExpenseCount = 0;
            }

            return View();
        }
    }
}
