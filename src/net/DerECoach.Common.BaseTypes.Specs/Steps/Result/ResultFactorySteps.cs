using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace DerECoach.Common.BaseTypes.Specs.Steps.Result
{
    [Binding]
    public class ResultFactorySteps
    {
        #region private fields ------------------------------------------------
        private const string RESULT_KEY = "Result";        
        private IResultFactory _resultFactory;
        #endregion

        #region helper properties ---------------------------------------------
        private IResult _result
        {
            get { return ScenarioContext.Current[RESULT_KEY] as IResult; }
        }

        private IResult<string> _resultWithReason
        {
            get { return ScenarioContext.Current[RESULT_KEY] as IResult<string>; }
        }

        private IResultEx<string, NullReferenceException> _resultWithReasonAndContext
        {
            get { return ScenarioContext.Current[RESULT_KEY] as IResultEx<string, NullReferenceException>; }
        }
        #endregion

        #region background ----------------------------------------------------
        [Given(@"I have initiated the ResultFactory")]
        public void GivenIHaveInitiatedTheResultFactory()
        {
            _resultFactory = ResultFactoryProvider.GetResultFactory();
        }
        #endregion

        #region Result --------------------------------------------------------
        [When(@"I call Success\(\)")]
        public void WhenICallSuccess()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success());
        }

        [When(@"I call Success\(message = ""(.*)""\)")]
        public void WhenICallSuccessMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success(p0));
        }

        [When(@"I call Success\(message1 = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallSuccessMessage1MessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success(p0, p1));
        }

        [When(@"I call Failure\(message = ""(.*)""\)")]
        public void WhenICallFailureMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(p0));
        }

        [When(@"I call Failure\(message1 = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureMessage1MessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(p0, p1));
        }

        [Then(@"an IResult is returned")]
        public void ThenAnIResultIsReturned()
        {
            Assert.IsInstanceOf(typeof(IResult), _result);
        }

        [Then(@"IResult\.Message is null")]
        public void ThenIResult_MessageIsNull()
        {
            Assert.IsNull(_result.Message);
        }

        [Then(@"IResult\.MessageLevel is ""(.*)""")]
        public void ThenIResult_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _result.MessageLevel);
        }

        [Then(@"IResult\.Succeeded is ""(.*)""")]
        public void ThenIResult_SucceededIs(bool p0)
        {
            Assert.AreEqual(p0, _result.Succeeded);
        }

        [Then(@"IResult\.Failed is ""(.*)""")]
        public void ThenIResult_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _result.Failed);
        }
        
        [Then(@"IResult\.Message is ""(.*)""")]
        public void ThenIResult_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _result.Message);
        }
        #endregion

        #region Result<TReason> -----------------------------------------------
        [When(@"I call Success\(string\)")]
        public void WhenICallSuccessString()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string>());
        }

        [When(@"I call Success\(string\)\(message = ""(.*)""\)")]
        public void WhenICallSuccessStringMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string>(p0));
        }

        [When(@"I call Success\(string\)\(message1 = ""(.*)"", messageLevel =  ""(.*)""\)")]
        public void WhenICallSuccessStringMessage1MessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string>(p0, p1));
        }

        [When(@"I call Failure\(string\)\(message = ""(.*)""\)")]
        public void WhenICallFailureStringMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string>(p0));
        }

        [When(@"I call Failure\(string\)\(message1 = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureStringMessage1MessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string>(p0, p1));
        }

        [When(@"I call Failure\(string\)\(reason = ""(.*)"", message = ""(.*)""\)")]
        public void WhenICallFailureStringReasonMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string>(p0, p1));
        }

        [When(@"I call Failure\(string\)\(reason = ""(.*)"", message1 = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureStringReasonMessage1MessageLevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string>(p0, p1, p2));
        }

        [Then(@"an IResult\(string\) is returned")]
        public void ThenAnIResultStringIsReturned()
        {
            Assert.IsInstanceOf(typeof(IResult<string>), _resultWithReason);
        }

        [Then(@"IResult\(string\)\.MessageLevel is ""(.*)""")]
        public void ThenIResultString_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _resultWithReason.MessageLevel);
        }

        [Then(@"IResult\(string\)\.Succeeded is ""(.*)""")]
        public void ThenIResultString_SucceededIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Succeeded);
        }

        [Then(@"IResult\(string\)\.Failed is ""(.*)""")]
        public void ThenIResultString_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Failed);
        }

        [Then(@"IResult\(string\)\.Reason is null")]
        public void ThenIResultString_ReasonIsNull()
        {
            Assert.IsNull(_resultWithReason.FailureReason);
        }
                
        [Then(@"IResult\(string\)\.Message is ""(.*)""")]
        public void ThenIResultString_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Message);
        }
                
        [Then(@"IResult\(string\)\.Reason is ""(.*)""")]
        public void ThenIResultString_ReasonIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReason.FailureReason);
        }
        #endregion

        #region Result<TReason, TContext> -------------------------------------
        [When(@"I call Success\(string, NullReferenceException\)")]
        public void WhenICallSuccessStringNullReferenceException()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string, NullReferenceException>());
        }

        [When(@"I call Success\(string, NullReferenceException\)\(message = ""(.*)""\)")]
        public void WhenICallSuccessStringNullReferenceExceptionMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string, NullReferenceException>(p0));
        }

        [When(@"I call Success\(string, NullReferenceException\)\(message1 = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallSuccessStringNullReferenceExceptionMessage1Messagelevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<string, NullReferenceException>(p0, p1));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(message = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(message1 = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionMessage1Messagelevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0, p1));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(reason = ""(.*)"", message = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionReasonMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0, p1));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(reason = ""(.*)"", message1 = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionReasonMessage1Messagelevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0, p1, p2));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(reason = ""(.*)"", new NullReferenceException, message = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionReasonNewNullReferenceExceptionMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0, new NullReferenceException(), p1));
        }

        [When(@"I call Failure\(string, NullReferenceException\)\(reason1 = ""(.*)"", new NullReferenceException, message = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureStringNullReferenceExceptionReason1NewNullReferenceExceptionMessageMessagelevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<string, NullReferenceException>(p0, new NullReferenceException(), p1, p2));
        }

        [Then(@"an IResultEx\(string, NullReferenceException\) is returned")]
        public void ThenAnIResultExStringNullReferenceExceptionIsReturned()
        {
            Assert.IsInstanceOf(typeof(IResultEx<string, NullReferenceException>), _resultWithReasonAndContext);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.MessageLevel is ""(.*)""")]
        public void ThenIResultExStringNullReferenceException_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.MessageLevel);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.Succeeded ""(.*)""")]
        public void ThenIResultExStringNullReferenceException_Succeeded(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Succeeded);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.Failed is ""(.*)""")]
        public void ThenIResultExStringNullReferenceException_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Failed);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.Reason is null")]
        public void ThenIResultExStringNullReferenceException_ReasonIsNull()
        {
            Assert.IsNull(_resultWithReasonAndContext.FailureReason);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.Context is null")]
        public void ThenIResultExStringNullReferenceException_ContextIsNull()
        {
            Assert.IsNull(_resultWithReasonAndContext.FailureContext);
        }

        [Then(@"IResultEx\(string, NullReferenceException\)\.Message is ""(.*)""")]
        public void ThenIResultExStringNullReferenceException_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Message);
        }
        
        [Then(@"IResultEx\(string, NullReferenceException\)\.Reason is ""(.*)""")]
        public void ThenIResultExStringNullReferenceException_ReasonIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.FailureReason);
        }
        
        [Then(@"IResultEx\(string, NullReferenceException\)\.Context is a NullReferenceException")]
        public void ThenIResultExStringNullReferenceException_ContextIsANullReferenceException()
        {
            Assert.IsInstanceOf(typeof(NullReferenceException), _resultWithReasonAndContext.FailureContext);
        }

        #endregion

    }
}
