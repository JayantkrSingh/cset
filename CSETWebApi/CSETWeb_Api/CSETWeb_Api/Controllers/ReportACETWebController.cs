﻿//////////////////////////////// 
// 
//   Copyright 2020 Battelle Energy Alliance, LLC  
// 
// 
//////////////////////////////// 
using CSETWeb_Api.BusinessLogic;
using CSETWeb_Api.BusinessLogic.Models;
using CSETWeb_Api.BusinessLogic.ReportEngine;
using CSETWeb_Api.BusinessManagers;
using CSETWeb_Api.Helpers;
using CSETWeb_Api.Models;
using DataLayerCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSETWeb_Api.Controllers
{
    public class ReportACETWebController : ApiController
    {
        [HttpGet]
        [Route("api/reports/acet/getDeficiencyList")]
        public ACETReportData GetDeficiencyList()
        {
            int assessmentId = Auth.AssessmentForUser();
            ReportsDataManager reportsDataManager = new ReportsDataManager(assessmentId);
            ACETReportData data = new ACETReportData();
            data.DeficiencesList = reportsDataManager.getACETDeficiences();            
            data.information = reportsDataManager.GetInformation();
            return data;
        }

        [HttpGet]
        [Route("api/reports/acet/getAltList")]
        public ACETReportData GetAltList()
        {
            int assessmentId = Auth.AssessmentForUser();
            ReportsDataManager reportsDataManager = new ReportsDataManager(assessmentId);
            ACETReportData data = new ACETReportData();
            data.AlternateList = reportsDataManager.getAlternatesList();
            data.information = reportsDataManager.GetInformation();
            return data;
        }
        [HttpGet]
        [Route("api/reports/acet/getCommentsMarked")]
        public ACETReportData GetCommentsMarked()
        {
            int assessmentId = Auth.AssessmentForUser();
            ReportsDataManager reportsDataManager = new ReportsDataManager(assessmentId);
            ACETReportData data = new ACETReportData();
            data.Comments = reportsDataManager.getCommentsList();
            data.MarkedForReviewList = reportsDataManager.getMarkedForReviewList();
            data.information = reportsDataManager.GetInformation();
            return data;
        }
        [HttpGet]
        [Route("api/reports/acet/GetAssessmentInfromation")]
        public ACETReportData GetAssessmentInfromation()
        {
            int assessmentId = Auth.AssessmentForUser();
            ReportsDataManager reportsDataManager = new ReportsDataManager(assessmentId);
            ACETReportData data = new ACETReportData();
            data.information = reportsDataManager.GetInformation();
            return data;
        }
        [HttpGet]
        [Route("api/reports/acet/getAnsweredQuestions")]
        public ACETReportData GetAnsweredQuestions()
        {
            int assessmentId = Auth.AssessmentForUser();
            ReportsDataManager reportsDataManager = new ReportsDataManager(assessmentId);
            ACETReportData data = new ACETReportData();
            //data.MatAnsweredQuestions = reportsDataManager.getAnsweredQuestionList();
            return data;
        }

    }
}
