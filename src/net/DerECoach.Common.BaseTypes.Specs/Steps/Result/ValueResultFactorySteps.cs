using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace DerECoach.Common.BaseTypes.Specs.Steps.Result
{
    [Binding]
    public class ValueResultFactorySteps
    {
        #region private fields ------------------------------------------------
        private const string RESULT_KEY = "Result";        
        private IValueResultFactory _resultFactory;
        #endregion

        #region helper properties ---------------------------------------------
        private IValueResult<Exception> _result
        {
            get { return ScenarioContext.Current[RESULT_KEY] as IValueResult<Exception>; }
        }

        private IValueResult<Exception, string> _resultWithReason
        {
            get
            {
                return ScenarioContext.Current[RESULT_KEY]
                    as IValueResult<Exception, string>;
            }
        }

        private IValueResultEx<Exception, string, NullReferenceException> _resultWithReasonAndContext
        {
            get
            {
                return ScenarioContext.Current[RESULT_KEY]
                    as IValueResultEx<Exception, string, NullReferenceException>;
            }
        }
        #endregion

        #region background ----------------------------------------------------
        [Given(@"I have initiated the ValueResultFactory")]
        public void GivenIHaveInitiatedTheValueResultFactory()
        {
            _resultFactory = ValueResultFactoryProvider.GetValueResultFactory();
        }
        #endregion

        #region Result<TValue> ------------------------------------------------
        [When(@"I call Success\(TValue\)\(\)")]
        public void WhenICallSuccessTValue()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success(new Exception()));
        }

        [When(@"I call Success\(TValue\)\(message = ""(.*)""\)")]
        public void WhenICallSuccessTValueMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success(new Exception(), p0));
        }

        [When(@"I call Success\(TValue\)\(tmessage = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallSuccessTValueMessageMessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue\)\(message = ""(.*)""\)")]
        public void WhenICallFailureTValueMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0));
        }

        [When(@"I call Failure\(TValue\)\(tmessage = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureTValueTMessageMessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0, p1));
        }

        [Then(@"an IValueResult\(TValue\) is returned")]
        public void ThenAnIValueResultTValueIsReturned()
        {
            Assert.IsInstanceOf(typeof(IValueResult<Exception>), _result);
        }

        [Then(@"IValueResult\(TValue\)\.Value is set")]
        public void ThenIValueResultTValue_ValueIsSet()
        {
            Assert.IsNotNull(_result.Value);
            Assert.IsInstanceOf(typeof(Exception), _result.Value);
        }

        [Then(@"IValueResult\(TValue\)\.Message is null")]
        public void ThenIValueResultTValue_MessageIsNull()
        {
            Assert.IsNull(_result.Message);
        }

        [Then(@"IValueResult\(TValue\)\.MessageLevel is ""(.*)""")]
        public void ThenIValueResultTValue_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _result.MessageLevel);                
        }

        [Then(@"IValueResult\(TValue\)\.Succeeded is ""(.*)""")]
        public void ThenIValueResultTValue_SucceededIs(bool p0)
        {
            Assert.AreEqual(p0, _result.Succeeded);
        }

        [Then(@"IValueResult\(TValue\)\.Failed is ""(.*)""")]
        public void ThenIValueResultTValue_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _result.Failed);
        }        

        [Then(@"IValueResult\(TValue\)\.Message is ""(.*)""")]
        public void ThenIValueResultTValue_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _result.Message);
        }
        #endregion

        #region Result<TValue, TReason> ---------------------------------------
        [When(@"I call Success\(TValue, string\)")]
        public void WhenICallSuccessTValueString()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string>(new Exception()));
        }

        [When(@"I call Success\(TValue, string\)\(message = ""(.*)""\)")]
        public void WhenICallSuccessTValueStringMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string>(new Exception(), p0));
        }

        [When(@"I call Success\(TValue, string\)\(tmessage = ""(.*)"", messageLevel =  ""(.*)""\)")]
        public void WhenICallSuccessTValueStringTMessageMessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string>(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string\)\(message = ""(.*)""\)")]
        public void WhenICallFailureTValueStringMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string>(new Exception(), p0));
        }

        [When(@"I call Failure\(TValue, string\)\(tmessage = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureTValueStringTMessageMessageLevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string>(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string\)\(reason = ""(.*)"", message = ""(.*)""\)")]
        public void WhenICallFailureTValueStringReasonMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string\)\(reason = ""(.*)"", tmessage = ""(.*)"", messageLevel = ""(.*)""\)")]
        public void WhenICallFailureTValueStringReasonTMessageMessageLevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0, p1, p2));
        }

        [Then(@"an IValueResult\(TValue, string\) is returned")]
        public void ThenAnIValueResultTValueStringIsReturned()
        {
            Assert.IsInstanceOf(typeof(IValueResult<Exception, string>), _resultWithReason);
        }

        [Then(@"IValueResult\(TValue, string\)\.Value is set")]
        public void ThenIValueResultTValueString_ValueIsSet()
        {
            Assert.IsNotNull(_resultWithReason.Value);
            Assert.IsInstanceOf(typeof(Exception), _resultWithReason.Value);
        }

        [Then(@"IValueResult\(TValue, string\)\.MessageLevel is ""(.*)""")]
        public void ThenIValueResultTValueString_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _resultWithReason.MessageLevel);
        }

        [Then(@"IValueResult\(TValue, string\)\.Succeeded is ""(.*)""")]
        public void ThenIValueResultTValueString_SucceededIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Succeeded);
        }

        [Then(@"IValueResult\(TValue, string\)\.Failed is ""(.*)""")]
        public void ThenIValueResultTValueString_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Failed);
        }

        [Then(@"IValueResult\(TValue, string\)\.Reason is null")]
        public void ThenIValueResultTValueString_ReasonIsNull()
        {
            Assert.IsNull(_resultWithReason.FailureReason);
        }        

        [Then(@"IValueResult\(TValue, string\)\.Reason is ""(.*)""")]
        public void ThenIValueResultTValueString_ReasonIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReason.FailureReason);
        }

        [Then(@"IValueResult\(TValue, string\)\.Message is ""(.*)""")]
        public void ThenIValueResultTValueString_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReason.Message);
        }
        #endregion

        #region Result<TValue, TReason, TContext> -------------------------------------

        [When(@"I call Success\(TValue, string, NullReferenceException\)")]
        public void WhenICallSuccessTValueStringNullReferenceException()
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string, NullReferenceException>(new Exception()));
        }

        [When(@"I call Success\(TValue, string, NullReferenceException\)\(message = ""(.*)""\)")]
        public void WhenICallSuccessTValueStringNullReferenceExceptionMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string, NullReferenceException>(new Exception(), p0));
        }

        [When(@"I call Success\(TValue, string, NullReferenceException\)\(tmessage = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallSuccessTValueStringNullReferenceExceptionTMessageMessagelevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Success<Exception, string, NullReferenceException>(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(message = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptionMessage(string p0)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string, NullReferenceException>(new Exception(), p0));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(tmessage = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptiontMessageMessagelevel(string p0, EMessageLevel p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string, NullReferenceException>(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(reason = ""(.*)"", message = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptionReasonMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string, NullReferenceException>(new Exception(), p0, p1));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(reason = ""(.*)"", tmessage = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptionReasonTMessageMessagelevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure<Exception, string, NullReferenceException>(new Exception(), p0, p1, p2));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(reason = ""(.*)"", new NullReferenceException, message = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptionReasonNewNullReferenceExceptionMessage(string p0, string p1)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0, new NullReferenceException(), p1));
        }

        [When(@"I call Failure\(TValue, string, NullReferenceException\)\(treason = ""(.*)"", new NullReferenceException, message = ""(.*)"", messagelevel = ""(.*)""\)")]
        public void WhenICallFailureTValueStringNullReferenceExceptiontReasonNewNullReferenceExceptionMessageMessagelevel(string p0, string p1, EMessageLevel p2)
        {
            ScenarioContext.Current.Add(
                RESULT_KEY,
                _resultFactory.Failure(new Exception(), p0, new NullReferenceException(), p1, p2));
        }

        [Then(@"an IValueResultEx\(TValue, string, NullReferenceException\) is returned")]
        public void ThenAnIValueResultExTValueStringNullReferenceExceptionIsReturned()
        {
            Assert.IsInstanceOf(typeof(IValueResultEx<Exception, string, NullReferenceException>), _resultWithReasonAndContext);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Value is set")]
        public void ThenIValueResultExTValueStringNullReferenceException_ValueIsSet()
        {
            Assert.IsNotNull(_resultWithReasonAndContext.Value);
            Assert.IsInstanceOf(typeof(Exception), _resultWithReasonAndContext.Value);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Context is a NullReferenceException")]
        public void ThenIValueResultExTValueStringNullReferenceException_ContextIsANullReferenceException()
        {
            Assert.IsInstanceOf(typeof(NullReferenceException), _resultWithReasonAndContext.FailureContext);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.MessageLevel is ""(.*)""")]
        public void ThenIValueResultExTValueStringNullReferenceException_MessageLevelIs(EMessageLevel p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.MessageLevel);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Succeeded ""(.*)""")]
        public void ThenIValueResultExTValueStringNullReferenceException_Succeeded(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Succeeded);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Failed is ""(.*)""")]
        public void ThenIValueResultExTValueStringNullReferenceException_FailedIs(bool p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Failed);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Reason is null")]
        public void ThenIValueResultExTValueStringNullReferenceException_ReasonIsNull()
        {
            Assert.IsNull(_resultWithReasonAndContext.FailureReason);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Context is null")]
        public void ThenIValueResultExTValueStringNullReferenceException_ContextIsNull()
        {
            Assert.IsNull(_resultWithReasonAndContext.FailureContext);
        }

        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Message is ""(.*)""")]
        public void ThenIValueResultExTValueStringNullReferenceException_MessageIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.Message);
        }
        
        [Then(@"IValueResultEx\(TValue, string, NullReferenceException\)\.Reason is ""(.*)""")]
        public void ThenIValueResultExTValueStringNullReferenceException_ReasonIs(string p0)
        {
            Assert.AreEqual(p0, _resultWithReasonAndContext.FailureReason);
        }

        #endregion
    }
}
