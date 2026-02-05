using Basic_Calculator.Persistence;
using Basic_Calculator.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); //tells ASP.NET that this application will use controllers as entry points
builder.Services.AddSwaggerGen();

//persistence file -> interface
builder.Services.AddSingleton<ICalculationStore>(new FileCalculationStore("Data/calculations.json"));//managing domain for you
//persistence file
builder.Services.AddSingleton<CalculatorService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();//calling the controllers that will be used eventually

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();//will always be the last line of the program.cs class

