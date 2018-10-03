Feature: ResultFactory
	Using the Resultfactory Interface for creating results

Background: 
	Given I have initiated the ResultFactory

###############################################################################
# Result class
###############################################################################
@Result
Scenario: Create a success Result without parameters	
	When I call Success()
	Then an IResult is returned
	And IResult.Message is null
	And IResult.MessageLevel is "None"
	And IResult.Succeeded is "true"
	And IResult.Failed is "false"

@Result
Scenario: Create a success Result with a message omitting the messagelevel parameter
	When I call Success(message = "the message")
	Then an IResult is returned
	And IResult.Message is "the message"
	And IResult.MessageLevel is "Info"	
	And IResult.Succeeded is "true"
	And IResult.Failed is "false"

@Result
Scenario: Create a success Result with a message and a messagelevel parameter
	When I call Success(message1 = "the message", messageLevel = "Verbose")
	Then an IResult is returned
	And IResult.Message is "the message"
	And IResult.MessageLevel is "Verbose"	
	And IResult.Succeeded is "true"
	And IResult.Failed is "false"

@Result
Scenario: Create a failure Result omitting the messagelevel parameter
	When I call Failure(message = "the message")
	Then an IResult is returned	
	And IResult.Message is "the message"
	And IResult.MessageLevel is "Error"
	And IResult.Succeeded is "false"
	And IResult.Failed is "true"

@Result
Scenario: Create a failure Result with a messagelevel parameter
	When I call Failure(message1 = "the message", messageLevel = "Verbose")
	Then an IResult is returned
	And IResult.Message is "the message"
	And IResult.MessageLevel is "Verbose"	
	And IResult.Succeeded is "false"
	And IResult.Failed is "true"

###############################################################################
# Result<TReason> class
###############################################################################
@Result<TReason>
Scenario: Create a success Result<TReason> without parameters
    When I call Success(string)
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "None"
	And IResult(string).Succeeded is "true"
	And IResult(string).Failed is "false"
	And IResult(string).Reason is null

@Result<TReason>
Scenario: Create a success Result<TReason> with a message omitting the messagelevel parameter	
	When I call Success(string)(message = "the message")
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Info"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "true"
	And IResult(string).Failed is "false"
	And IResult(string).Reason is null

@Result<TReason>
Scenario: Create a success Result<TReason> with a message and a messagelevel parameter
	When I call Success(string)(message1 = "the message", messageLevel =  "Verbose")
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Verbose"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "true"
	And IResult(string).Failed is "false"
	And IResult(string).Reason is null

@Result<TReason>
Scenario: Create a failure Result<TReason> omitting the messagelevel parameter
	When I call Failure(string)(message = "the message")
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Error"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "false"
	And IResult(string).Failed is "true"
	And IResult(string).Reason is null

@Result<TReason>
Scenario: Create a failure Result<TReason> with the messagelevel parameter set
	When I call Failure(string)(message1 = "the message", messageLevel = "Verbose")
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Verbose"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "false"
	And IResult(string).Failed is "true"
	And IResult(string).Reason is null

@Result<TReason>
Scenario: Create a failure Result<TReason> with a reason omitting the messagelevel parameter
    When I call Failure(string)(reason = "the reason", message = "the message") 	
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Error"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "false"
	And IResult(string).Failed is "true"
	And IResult(string).Reason is "the reason"

@Result<TReason>
Scenario: Create a failure Result<TReason> with a reason with the messagelevel parameter set
	When I call Failure(string)(reason = "the reason", message1 = "the message", messageLevel = "Verbose") 	
	Then an IResult(string) is returned
	And IResult(string).MessageLevel is "Verbose"
	And IResult(string).Message is "the message"
	And IResult(string).Succeeded is "false"
	And IResult(string).Failed is "true"
	And IResult(string).Reason is "the reason"

###############################################################################
# ResultEx<TReason, TContext> class
###############################################################################
@Result<TReason, TContext>
Scenario: Create a success Result<TReason, TContext> without parameters
    When I call Success(string, NullReferenceException)
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "None"
	And IResultEx(string, NullReferenceException).MessageLevel is "None"
	And IResultEx(string, NullReferenceException).Succeeded "true"
	And IResultEx(string, NullReferenceException).Failed is "false"
	And IResultEx(string, NullReferenceException).Reason is null
	And IResultEx(string, NullReferenceException).Context is null

@Result<TReason, TContext>
Scenario: Create a success Result<TReason, TContext> with a message	omitting the messagelevel parameter
	When I call Success(string, NullReferenceException)(message = "the message")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Info"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "true"
	And IResultEx(string, NullReferenceException).Failed is "false"
	And IResultEx(string, NullReferenceException).Reason is null
	And IResultEx(string, NullReferenceException).Context is null

@Result<TReason, TContext>
Scenario: Create a success Result<TReason, TContext> with a message	with the messagelevel parameter set
	When I call Success(string, NullReferenceException)(message1 = "the message", messagelevel = "Verbose")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Verbose"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "true"
	And IResultEx(string, NullReferenceException).Failed is "false"
	And IResultEx(string, NullReferenceException).Reason is null
	And IResultEx(string, NullReferenceException).Context is null

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> without messagelevel, reason or context
	When I call Failure(string, NullReferenceException)(message = "the message")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Error"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is null
	And IResultEx(string, NullReferenceException).Context is null

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> with messagelevel, but without reason or context
	When I call Failure(string, NullReferenceException)(message1 = "the message", messagelevel = "Verbose")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Verbose"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is null
	And IResultEx(string, NullReferenceException).Context is null

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> with a reason, but without context or messagelevel
	When I call Failure(string, NullReferenceException)(reason = "the reason", message = "the message")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Error"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is "the reason" 
	And IResultEx(string, NullReferenceException).Context is null	

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> with a reason and a messagelevel, but without context
	When I call Failure(string, NullReferenceException)(reason = "the reason", message1 = "the message", messagelevel = "Verbose")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Verbose"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is "the reason"
	And IResultEx(string, NullReferenceException).Context is null	

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> with a reason and a context, but without messagelevel
	When I call Failure(string, NullReferenceException)(reason = "the reason", new NullReferenceException, message = "the message")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Error"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is "the reason"
	And IResultEx(string, NullReferenceException).Context is a NullReferenceException

@Result<TReason, TContext>
Scenario: Create a failure Result<TReason, TContext> with a reason, context, message And MessageLevel set
	When I call Failure(string, NullReferenceException)(reason1 = "the reason", new NullReferenceException, message = "the message", messagelevel = "Verbose")
	Then an IResultEx(string, NullReferenceException) is returned
	And IResultEx(string, NullReferenceException).MessageLevel is "Verbose"
	And IResultEx(string, NullReferenceException).Message is "the message"
	And IResultEx(string, NullReferenceException).Succeeded "false"
	And IResultEx(string, NullReferenceException).Failed is "true"
	And IResultEx(string, NullReferenceException).Reason is "the reason"
	And IResultEx(string, NullReferenceException).Context is a NullReferenceException
