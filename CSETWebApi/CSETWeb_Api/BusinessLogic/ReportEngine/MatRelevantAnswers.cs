﻿//////////////////////////////// 
// 
//   Copyright 2020 Battelle Energy Alliance, LLC  
// 
// 
//////////////////////////////// 
using DataLayerCore.Model;
using System.Collections.Generic;


namespace CSETWeb_Api.BusinessLogic.ReportEngine
{
    public class MatRelevantAnswers
    {
        public MatRelevantAnswers()
        {
        }

        public ANSWER ANSWER { get; set; }
        public MATURITY_QUESTIONS Mat { get; set; }
    }

    public class MatAnsweredQuestionDomain
    {
        public MatAnsweredQuestionDomain()
        {
        }

        public string Title { get; set; }
        public List<MaturityAnsweredQuestionsAssesment> AssesmentFactor { get; set; }
    }

    public class MaturityAnsweredQuestionsAssesment
    {
        public MaturityAnsweredQuestionsAssesment()
        {
        }
        public string Title { get; set; }
        public List<MaturityAnsweredQuestionsComponent> Component { get; set; }
    }
    public class MaturityAnsweredQuestionsComponent
    {
        public MaturityAnsweredQuestionsComponent()
        {
        }
        public string Title { get; set; }
        public List<MaturityAnsweredQuestions> Questions { get; set; }
    }

    public class MaturityAnsweredQuestions
    {
        public MaturityAnsweredQuestions()
        {
        }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public string MaturityLevel { get; set; }
        public string Comments { get; set; }
    }

}