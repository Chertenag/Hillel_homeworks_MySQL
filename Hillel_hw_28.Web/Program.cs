
namespace Hillel_hw_28.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Hillel_hw_23.Core.Settings.ConnectionStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}