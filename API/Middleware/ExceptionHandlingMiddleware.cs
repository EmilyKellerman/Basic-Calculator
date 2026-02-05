using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;//call the function that will process the http request
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "aplication/json";
        
        response.StatusCode = exception switch
        {
            InvalidCalculationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        var payload = new
        {
            error = exception.GetType().Name, //example: 400
            detail = exception.Message
        };
        return response.WriteAsync(JsonSerializer.Serialize(payload));
    }//HandleExceptionAsync

    public ExceptionHandlingMiddleware (RequestDelegate next) //pass function to the middleware
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) //context = request / response
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

}