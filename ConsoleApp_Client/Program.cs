
using ConsoleApp_ClintTest_0;
using ConsoleApp_ClintTest_0.Api;
using ConsoleApp_ClintTest_0.Services;
using ConsoleApp_ClintTest_0.Services.Interfaces;
using ConsoleApp_ClintTest_0.Students;
using ConsoleClient.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//var services = new ServiceCollection();

//services.AddHttpClient<WebAppApiClient>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7079/");
//});

//services.AddTransient<AuthService>();
//services.AddTransient<LoginScreen>();

//using var provider = services.BuildServiceProvider();

//var loginScreen = provider.GetRequiredService<LoginScreen>();
//var authService = provider.GetRequiredService<AuthService>();

//bool isLoggedIn = await loginScreen.ShowAsync();

//while (!isLoggedIn)
//{
//    Console.WriteLine();
//    Console.WriteLine("The email or password is wrong!");
//    Console.Write("Try again? (y/n): ");

//    var answer = Console.ReadLine();

//    if (!string.Equals(answer?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
//        break;

//    isLoggedIn = await loginScreen.ShowAsync();
//}

//if (isLoggedIn) await Run.Start();

public class Program
{

    static HttpClient GetHttpClientAuth = new HttpClient();
    public static AuthService? authService = new AuthService(GetHttpClientAuth);
    public static LoginScreen? loginScreen = new LoginScreen(authService);


    public static async Task Main(string[] args)
    {

        /*

                var services = new ServiceCollection();

                services.AddHttpClient<WebAppApiClient>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7079/");
                });

                services.AddSingleton<AuthService>();
                services.AddTransient<LoginScreen>();

                using var provider = services.BuildServiceProvider();

                var loginScreen = provider.GetRequiredService<LoginScreen>();
                var authService = provider.GetRequiredService<AuthService>();

                _authService = authService;
                _loginScreen = loginScreen;

         */

        if (loginScreen is null)
        {
            Console.WriteLine("the loging is faild !!");
            return;
        }

        bool isLoggedIn = await loginScreen.ShowAsync();

        while (!isLoggedIn)
        {
            Console.WriteLine();
            Console.WriteLine("The email or password is wrong!");
            Console.Write("Try again? (y/n): ");

            var answer = Console.ReadLine();

            if (!string.Equals(
                    answer?.Trim(),
                    "y",
                    StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            isLoggedIn = await loginScreen.ShowAsync();
        }

        if (isLoggedIn)
        {
            Students.StudentClient = new HttpClient();
            if (Students.StudentClient is not null)
            {
                Students.StudentClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Program.authService!.AccessToken);
                await Run.Start();
            }
        }
    }

}


