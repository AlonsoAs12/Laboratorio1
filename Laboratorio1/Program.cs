using System;

public class CuentaBancaria
{
    public string NumeroCuenta { get; set; }
    public string TitularCuenta { get; set; }
    public decimal Saldo { get; set; }
    public string PinSeguridad { get; set; }

    public CuentaBancaria(string numeroCuenta, string titularCuenta, decimal saldo, string pinSeguridad)
    {
        NumeroCuenta = numeroCuenta;
        TitularCuenta = titularCuenta;
        Saldo = saldo;
        PinSeguridad = pinSeguridad;
    }
}

public class CajeroAutomatico : CuentaBancaria
{
    private const int IntentosMaximos = 3;
    private int intentosFallidos;

    public CajeroAutomatico(string numeroCuenta, string titularCuenta, decimal saldo, string pinSeguridad)
        : base(numeroCuenta, titularCuenta, saldo, pinSeguridad)
    {
        intentosFallidos = 0;
    }

    public void MostrarSaldo()
    {
        Console.WriteLine($"Saldo actual: {Saldo:C}");
    }

    public void AgregarFondos(decimal cantidad)
    {
        Saldo += cantidad;
        Console.WriteLine($"Se agregaron {cantidad:C} a la cuenta.");
    }

    public void RetirarFondos(decimal cantidad)
    {
        if (cantidad > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente.");
        }

        Saldo -= cantidad;
        Console.WriteLine($"Se retiraron {cantidad:C} de la cuenta.");
    }

    public void CambiarPin(string nuevoPin)
    {
        if (nuevoPin == PinSeguridad)
        {
            throw new InvalidOperationException("El nuevo PIN no puede ser igual al anterior.");
        }

        PinSeguridad = nuevoPin;
        Console.WriteLine("PIN cambiado exitosamente.");
    }

    public void RealizarTransaccion(string pin)
    {
        if (pin != PinSeguridad)
        {
            intentosFallidos++;
            if (intentosFallidos >= IntentosMaximos)
            {
                throw new InvalidOperationException("Demasiados intentos fallidos. La cuenta ha sido bloqueada.");
            }
            throw new InvalidOperationException("PIN incorrecto.");
        }

        Console.WriteLine("Ingrese el número correspondiente a la transacción que desea realizar:");
        Console.WriteLine("1. Mostrar saldo");
        Console.WriteLine("2. Agregar fondos");
        Console.WriteLine("3. Retirar fondos");
        Console.WriteLine("4. Cambiar PIN");

        int opcion = int.Parse(Console.ReadLine());

        switch (opcion)
        {
            case 1:
                MostrarSaldo();
                break;
            case 2:
                Console.WriteLine("Ingrese la cantidad a depositar:");
                decimal deposito = decimal.Parse(Console.ReadLine());
                AgregarFondos(deposito);
                break;
            case 3:
                Console.WriteLine("Ingrese la cantidad a retirar:");
                decimal retiro = decimal.Parse(Console.ReadLine());
                RetirarFondos(retiro);
                break;
            case 4:
                Console.WriteLine("Ingrese el nuevo PIN:");
                string nuevoPin = Console.ReadLine();
                CambiarPin(nuevoPin);
                break;
            default:
                throw new InvalidOperationException("Opción inválida.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {

        CajeroAutomatico cuenta = new CajeroAutomatico("123456789", "Alonso Asencios", 1000, "1234");

        Console.WriteLine("Ingrese su PIN para acceder a la cuenta:");
        string pin = Console.ReadLine();

        try
        {
            cuenta.RealizarTransaccion(pin);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
