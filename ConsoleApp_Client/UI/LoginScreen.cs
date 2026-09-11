using ConsoleApp_ClintTest_0.DTOs.Auth;
using ConsoleApp_ClintTest_0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleClient.UI;

public class LoginScreen
{
    
    private readonly AuthService _authService;

    public LoginScreen(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<bool> ShowAsync()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔================================╗");
        Console.WriteLine("║         STUDENT PORTAL         ║");
        Console.WriteLine("╚================================╝");
        Console.ResetColor();

        Console.WriteLine();

        Console.Write("Email: ");
        var email = Console.ReadLine();

        Console.Write("Password: ");
        var password = ReadPassword();

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nEmail and password are required.");
            Console.ResetColor();

            return false;
        }

        var request = new LoginRequest
        {
            Email = email,
            Password = password
        };

        try
        {
            Console.WriteLine("\nLogging in...");

            var isResponsed =
                await _authService.LoginAsync(email,password);

            return isResponsed;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nLogin failed: {ex.Message}");
            Console.ResetColor();

            return false;
        }
    }

    private static string ReadPassword()
    {
        var password = string.Empty;

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }

                continue;
            }

            if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }

}
