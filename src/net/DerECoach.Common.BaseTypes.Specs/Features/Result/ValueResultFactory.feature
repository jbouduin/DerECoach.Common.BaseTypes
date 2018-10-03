Feature: ValueResultFactory
	Using the ValueResultfactory Interface for creating ValueResults

Background: 
	Given I have initiated the ValueResultFactory

###############################################################################
# ValueResult class
###############################################################################
@ValueResult<TValue>
Scenario: Create a success ValueResult without parameters	
	When I call Success(TValue)()
	Then an IValueResult(TValue) is returned
	And IValueResult(TValue).Value is set
	And IValueResult(TValue).Message is null
	And IValueResult(TValue).MessageLevel is "None"
	And IValueResult(TValue).Succeeded is "true"
	And IValueResult(TValue).Failed is "false"

@ValueResult<TValue>
Scenario: Create a success ValueResult with a message omitting the messagelevel parameter
	When I call Success(TValue)(message = "the message")
	Then an IValueResult(TValue) is returned
	And IValueResult(TValue).Value is set
	And IValueResult(TValue).Message is "the message"
	And IValueResult(TValue).MessageLevel is "Info"	
	And IValueResult(TValue).Succeeded is "true"
	And IValueResult(TValue).Failed is "false"

@ValueResult<TValue>
Scenario: Create a success ValueResult with a message and a messagelevel parameter
	When I call Success(TValue)(tmessage = "the message", messageLevel = "Verbose")
	Then an IValueResult(TValue) is returned
	And IValueResult(TValue).Value is set
	And IValueResult(TValue).Message is "the message"
	And IValueResult(TValue).MessageLevel is "Verbose"	
	And IValueResult(TValue).Succeeded is "true"
	And IValueResult(TValue).Failed is "false"

@ValueResult<TValue>
Scenario: Create a failure ValueResult omitting the messagelevel parameter
	When I call Failure(TValue)(message = "the message")
	Then an IValueResult(TValue) is returned
	And IValueResult(TValue).Value is set
	And IValueResult(TValue).Message is "the message"
	And IValueResult(TValue).MessageLevel is "Error"
	And IValueResult(TValue).Succeeded is "false"
	And IValueResult(TValue).Failed is "true"

@ValueResult<TValue>
Scenario: Create a failure ValueResult with a messagelevel parameter
	When I call Failure(TValue)(tmessage = "the message", messageLevel = "Verbose")
	Then an IValueResult(TValue) is returned
	And IValueResult(TValue).Value is set
	And IValueResult(TValue).Message is "the message"
	And IValueResult(TValue).MessageLevel is "Verbose"	
	And IValueResult(TValue).Succeeded is "false"
	And IValueResult(TValue).Failed is "true"

###############################################################################
# ValueResult<TReason> class
###############################################################################
@Result<TValue,TReason>
Scenario: Create a success ValueResult<TReason> without parameters
    When I call Success(TValue, string)
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "None"
	And IValueResult(TValue, string).Succeeded is "true"
	And IValueResult(TValue, string).Failed is "false"
	And IValueResult(TValue, string).Reason is null

@Result<TValue,TReason>
Scenario: Create a success ValueResult<TReason> with a message omitting the messagelevel parameter	
	When I call Success(TValue, string)(message = "the message")
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Info"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "true"
	And IValueResult(TValue, string).Failed is "false"
	And IValueResult(TValue, string).Reason is null

@Result<TValue,TReason>
Scenario: Create a success ValueResult<TReason> with a message and a messagelevel parameter
	When I call Success(TValue, string)(tmessage = "the message", messageLevel =  "Verbose")
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Verbose"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "true"
	And IValueResult(TValue, string).Failed is "false"
	And IValueResult(TValue, string).Reason is null

@Result<TValue,TReason>
Scenario: Create a failure ValueResult<TReason> omitting the messagelevel parameter
	When I call Failure(TValue, string)(message = "the message")
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Error"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "false"
	And IValueResult(TValue, string).Failed is "true"
	And IValueResult(TValue, string).Reason is null

@Result<TValue,TReason>
Scenario: Create a failure ValueResult<TReason> with the messagelevel parameter set
	When I call Failure(TValue, string)(tmessage = "the message", messageLevel = "Verbose")
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Verbose"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "false"
	And IValueResult(TValue, string).Failed is "true"
	And IValueResult(TValue, string).Reason is null

@Result<TValue,TReason>
Scenario: Create a failure ValueResult<TReason> with a reason omitting the messagelevel parameter
    When I call Failure(TValue, string)(reason = "the reason", message = "the message") 	
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Error"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "false"
	And IValueResult(TValue, string).Failed is "true"
	And IValueResult(TValue, string).Reason is "the reason"

@Result<TValue,TReason>
Scenario: Create a failure ValueResult<TReason> with a reason with the messagelevel parameter set
	When I call Failure(TValue, string)(reason = "the reason", tmessage = "the message", messageLevel = "Verbose") 	
	Then an IValueResult(TValue, string) is returned
	And IValueResult(TValue, string).Value is set
	And IValueResult(TValue, string).MessageLevel is "Verbose"
	And IValueResult(TValue, string).Message is "the message"
	And IValueResult(TValue, string).Succeeded is "false"
	And IValueResult(TValue, string).Failed is "true"
	And IValueResult(TValue, string).Reason is "the reason"

###############################################################################
# ValueResultEx<TValue, TReason, TContext> class
###############################################################################
@Result<TValue, TReason, TContext>
Scenario: Create a success ValueResult<TReason, TContext> without parameters
    When I call Success(TValue, string, NullReferenceException)
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "None"
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "None"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "true"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "false"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is null
	And IValueResultEx(TValue, string, NullReferenceException).Context is null

@Result<TValue, TReason, TContext>
Scenario: Create a success ValueResult<TReason, TContext> with a message	omitting the messagelevel parameter
	When I call Success(TValue, string, NullReferenceException)(message = "the message")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Info"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "true"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "false"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is null
	And IValueResultEx(TValue, string, NullReferenceException).Context is null

@Result<TValue, TReason, TContext>
Scenario: Create a success ValueResult<TReason, TContext> with a message	with the messagelevel parameter set
	When I call Success(TValue, string, NullReferenceException)(tmessage = "the message", messagelevel = "Verbose")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Verbose"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "true"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "false"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is null
	And IValueResultEx(TValue, string, NullReferenceException).Context is null

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> without messagelevel, reason or context
	When I call Failure(TValue, string, NullReferenceException)(message = "the message")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Error"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is null
	And IValueResultEx(TValue, string, NullReferenceException).Context is null

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> with messagelevel, but without reason or context
	When I call Failure(TValue, string, NullReferenceException)(tmessage = "the message", messagelevel = "Verbose")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Verbose"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is null
	And IValueResultEx(TValue, string, NullReferenceException).Context is null

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> with a reason, but without context or messagelevel
	When I call Failure(TValue, string, NullReferenceException)(reason = "the reason", message = "the message")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Error"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is "the reason" 
	And IValueResultEx(TValue, string, NullReferenceException).Context is null	

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> with a reason and a messagelevel, but without context
	When I call Failure(TValue, string, NullReferenceException)(reason = "the reason", tmessage = "the message", messagelevel = "Verbose")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Verbose"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is "the reason"
	And IValueResultEx(TValue, string, NullReferenceException).Context is null	

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> with a reason and a context, but without messagelevel
	When I call Failure(TValue, string, NullReferenceException)(reason = "the reason", new NullReferenceException, message = "the message")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Error"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is "the reason"
	And IValueResultEx(TValue, string, NullReferenceException).Context is a NullReferenceException

@Result<TValue, TReason, TContext>
Scenario: Create a failure ValueResult<TReason, TContext> with a reason, context, message And MessageLevel set
	When I call Failure(TValue, string, NullReferenceException)(treason = "the reason", new NullReferenceException, message = "the message", messagelevel = "Verbose")
	Then an IValueResultEx(TValue, string, NullReferenceException) is returned
	And IValueResultEx(TValue, string, NullReferenceException).Value is set
	And IValueResultEx(TValue, string, NullReferenceException).MessageLevel is "Verbose"
	And IValueResultEx(TValue, string, NullReferenceException).Message is "the message"
	And IValueResultEx(TValue, string, NullReferenceException).Succeeded "false"
	And IValueResultEx(TValue, string, NullReferenceException).Failed is "true"
	And IValueResultEx(TValue, string, NullReferenceException).Reason is "the reason"
	And IValueResultEx(TValue, string, NullReferenceException).Context is a NullReferenceException
